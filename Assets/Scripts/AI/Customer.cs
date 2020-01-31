using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    // vars
    float waitedFor;
    [SerializeField] Waypoint target;

    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        //TODO: Get an ID from the GameManager?
        //TODO: Decide how much money they have

        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.SetDestination(target.Position);
    }

    // Update is called once per frame
    void Update()
    {
        //If customer has reached destination
        if(agent.remainingDistance < 0.1f)
        {
            //Request next destiation
            target.Leave(this);
            target = target.GetNext();
            target.Request(this);
        }

        //If target node is ready for new customer
        if (target.Ready(this))
            agent.SetDestination(target.Position);
        else
            waitedFor += Time.deltaTime;
    }
}
