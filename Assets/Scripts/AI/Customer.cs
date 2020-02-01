using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum Behaviour { Queueing, Ordering, Drinking };
public enum AnimState { Idle, Walking };
public class Customer : MonoBehaviour
{
    public Order order;
    // vars
    float waitedFor;
    [SerializeField] Waypoint target;

    public Behaviour behaviour;
    public AnimState animState;

    NavMeshAgent agent;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        //Init animation stuff
        animator = GetComponentInChildren<Animator>();
        animState = AnimState.Idle;

        //Init nav stuff
        CustomerManager.Instance.Register(this);
        target = CustomerManager.Instance.JoinQueue(this);
        target.Request(this);
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.SetDestination(target.Position);

        behaviour = Behaviour.Queueing;        
    }
    void SetAnimState(AnimState value)
    {
        animState = value;
        animator.SetInteger("State", (int)value);
        Debug.Log("AnimState updated: #" + (int)value +"_"+ value.ToString());
        //Debug.Log(agent.velocity.sqrMagnitude);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (agent.velocity.sqrMagnitude > 0.1f)
            SetAnimState(AnimState.Walking);
        else
            SetAnimState(AnimState.Idle);

        switch (behaviour)
        {
            case Behaviour.Queueing:
                InQueue();
                break;
            case Behaviour.Ordering:
                AtBar();
                break;
            case Behaviour.Drinking:
                Drinking();
                break;
        }
    }
    
    void InQueue()
    {
        //If customer has reached destination
        if (agent.remainingDistance < 0.1f)
        {
            target.Leave(this);

            //Try to advance in the queue
            //If no more spots, we're at the bar
            if ((target = target.GetNext()) == null)
            {
                behaviour = Behaviour.Ordering;
            }

            //Tell target we are on our way
            target.Request(this);
        }

        //If target node is ready for new customer
        if (target.Ready(this))
        {
            agent.SetDestination(target.Position);
        }
        else
            waitedFor += Time.deltaTime;
    }

    public float temp_fakebartime = 5.0f;
    void AtBar()
    {
        animState = AnimState.Idle;
        if (temp_fakebartime > 0)
        {
            temp_fakebartime -= Time.deltaTime;
        }
        else
        {
            
            target = CustomerManager.Instance.RequestIdleSpot(this);
            //If target node is ready for new customer
            if (target.Ready(this))
            {
                agent.SetDestination(target.Position);
                
            }
        }        
    }

    float temp_fakedrinktime = 10.0f;
    void Drinking()
    {
        if (temp_fakedrinktime > 0)
            temp_fakedrinktime -= Time.deltaTime;
        else
        {
            target = CustomerManager.Instance.JoinQueue(this);
            temp_fakebartime = 5.0f;
            temp_fakedrinktime = 5.0f;
        }            
    }
}
