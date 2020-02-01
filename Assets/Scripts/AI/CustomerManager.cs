using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    //List<Customer> customers = new List<Customer>();
    [SerializeField] List<Waypoint> queueSpots; //bad name - this only stores the end or "top" of the queue, once customer is inside they figure their own way through
    [SerializeField] List<Waypoint> idleSpots;
    [SerializeField] List<Waypoint> barSpots;

    #region Singleton
    static CustomerManager _instance = null;

    public static CustomerManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<CustomerManager>();
            }                
            if(_instance == null)//still
            {
                GameObject go = new GameObject();
                _instance = go.AddComponent<CustomerManager>();
            }
            return _instance;
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //customers = new List<Customer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
    public void Register(Customer customer)
    {
        if (customers.Contains(customer))
            Debug.Log("Warning: Double-register attempted: " + customer.name);
        else
            customers.Add(customer);
    }

    public Waypoint JoinQueue(Customer customer)
    {
        Waypoint wp;
        do
        {
            wp = queueSpots[Random.Range(0,queueSpots.Count)];
        } while (wp.Occupied);
        customer.behaviour = Behaviour.Queueing;
        return wp;
    }*/

    public Waypoint RequestQueueSpot()
    {
        //TODO: shuffle
        foreach (Waypoint wp in queueSpots)
            if (!wp.Occupied)
                return wp;

        return null;
    }
    /*
    public Waypoint RequestIdleSpot(Customer customer)
    {
        foreach (Waypoint x in queueSpots)
            x.customers.Remove(customer);
        foreach (Waypoint x in barSpots)
            x.customers.Remove(customer);

        Waypoint wp;
        do
        {
            wp = idleSpots[Random.Range(0, idleSpots.Count)];
        } while (wp.Occupied);
        customer.behaviour = Behaviour.Drinking;
        return wp;
    }*/
    public Waypoint RequestIdleSpot()
    {
        Waypoint wp;
        do
        {
            wp = idleSpots[Random.Range(0, idleSpots.Count)];
        } while (wp.Occupied);
        return wp;
    }
}
