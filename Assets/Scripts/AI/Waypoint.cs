using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] public bool barSpot;
    [SerializeField] List<Waypoint> nextWaypoint;
    List<Customer> customers = new List<Customer>();

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
    }

    public bool Occupied => customers.Count > 0;

    //GIZMOS STUFF
    Color nodeDefault = Color.white;
    Color nodeOccupied = Color.white;
    private void OnDrawGizmos()
    {
        Gizmos.color = Occupied ? nodeOccupied : nodeDefault;
        Gizmos.DrawWireSphere(gameObject.transform.position, 0.5f);
        Gizmos.color = Color.green;
        foreach (Waypoint wp in nextWaypoint)
            Gizmos.DrawLine(gameObject.transform.position, wp.gameObject.transform.position);
    }
}
