using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    // vars
    float waitedFor;
    [SerializeField] Waypoint target;
    [SerializeField] Waypoint current;

    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        //TODO: Get an ID from the GameManager?
        //TODO: Decide how much money they have

        agent = gameObject.GetComponent<NavMeshAgent>();
        //agent.SetDestination(target.Position);
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance < 0.1f)
        {
            //Reached target
            current = target;
            current.Enter(this);

            //Request next target
            target = current.GetNext();
            agent.SetDestination(target.Position);
            agent.st
        }
    }
}
