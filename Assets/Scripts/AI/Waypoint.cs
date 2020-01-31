using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] List<Waypoint> nextWaypoint;
    Customer currentCustomer;
    List<Customer> requests;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //Quick ref to pos
    public Vector3 Position => gameObject.transform.position;

    //TODO: Validate spot is occupied or not
    public Waypoint GetNext()
    {
        return nextWaypoint[Random.Range(0, nextWaypoint.Count - 1)];
    }

    public void Request(Customer customer)
    {
        requests.Add(customer);
    }
    public void Enter(Customer customer)
    {
        currentCustomer = customer;
    }
    public void Leave(Customer customer)
    {
        if (customer == currentCustomer)
            currentCustomer = null;
        else if(requests.Contains(customer))
            requests.Remove(customer);
        else
            Debug.Log("HOW CAN YOU LEAVE??? YOU WERE NEVER HERE!!!");

        //Reset customer just to be sure
        currentCustomer = requests[0];
    }
}
