using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerController : MonoBehaviour
{
    public Order order;
    Animator animator;
    NavMeshAgent agent;
    public Waypoint target;
    public bool serviceComplete;
    public bool atBar;
    [Range(0.0f,1.0f)]
    public float drunkness;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        
        //-----
        JoinQueue();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Idle", agent.velocity.sqrMagnitude < 0.1f);
            

        if(target!=null)
        {
            Waypoint wp = target.EvaluateNext();

            if (wp != null)
            {
                SetTarget(wp);
            }
            if(target.barSpot && agent.remainingDistance < 0.1f)
            {
                //target.Occupied = false;
                //target = null;
                atBar = true;
            }
        }
        if(atBar && serviceComplete)
        {
            atBar = false;
            SetTarget(CustomerManager.Instance.RequestIdleSpot());
        }


    }

    void SetTarget(Waypoint wp)
    {
        //Leave old wp
        if (target)
            target.Occupied = false;

        //Join new wp
        target = wp;
        agent.SetDestination(target.transform.position);
        target.Occupied = true;
    }


    IEnumerator Wait(float t)
    {
        yield return new WaitForSeconds(t);
        JoinQueue();
    }
    void JoinQueue()
    {
        Waypoint wp = CustomerManager.Instance.RequestQueueSpot();
        if (wp == null)
            StartCoroutine(Wait(0.2f));
        else
            SetTarget(wp);
    }

    [ContextMenu("Finish Drink")]
    public void FinishedDrink()
    {
        serviceComplete = false;
        JoinQueue();
    }

    public void SetDrunkness(float v)
    {
        drunkness = v;
        animator.SetFloat("Drunkness", drunkness);
    }
#if UNITY_EDITOR
    [ContextMenu("Update Drunkness")]
    public void UpdateDrunkness()
    {
        animator.SetFloat("Drunkness", drunkness);
    }
#endif
}
