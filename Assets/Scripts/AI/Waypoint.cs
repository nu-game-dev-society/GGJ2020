using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] List<Waypoint> nextWaypoint;
    List<Customer> customers;

    // Start is called before the first frame update
    void Start()
    {
        customers = new List<Customer>();
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
        if (nextWaypoint.Count == 0)
            return this;
        else
        {
            Waypoint wp;
            do
            {
                wp = nextWaypoint[Random.Range(0, nextWaypoint.Count)];
            } while (wp.Occupied);
            return wp;
        }
    }

    public void Request(Customer customer)
    {
        customers.Add(customer);
    }

    public bool Ready(Customer customer)
    {
        if (customers.Count > 0)
            return customers[0] == customer;
        else
            return true;
    }

    public void Leave(Customer customer)
    {
        if(customers.Contains(customer))
            customers.Remove(customer);
        else
            Debug.Log("HOW CAN YOU LEAVE??? YOU WERE NEVER HERE!!!");
    }

    public bool Occupied => customers.Count > 0;
}
