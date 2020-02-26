using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
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
    public Waypoint RequestQueueSpot()
    {
        foreach (Waypoint wp in queueSpots)
            if (!wp.Occupied)
                return wp;

        return null;
    }
    
    public Waypoint RequestIdleSpot()
    {
        Waypoint wp;
        do
        {
            wp = idleSpots[Random.Range(0, idleSpots.Count)];
        } while (wp.Occupied);
        return wp;
    }

    public List<CustomerController> GetAllCustomers()
    {
        return null;
    }
}
