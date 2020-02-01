using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] public bool barSpot;
    [SerializeField] List<Waypoint> nextWaypoint = new List<Waypoint>();
    public bool Occupied;
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
            return null;

        Waypoint wp;
        do
        {
            wp = nextWaypoint[Random.Range(0, nextWaypoint.Count)];
        } while (wp.Occupied);
        return wp;
    }

    public Waypoint EvaluateNext()
    {
        if (nextWaypoint.Count == 0)
            return null;

        Waypoint wp = null;
        foreach (Waypoint x in nextWaypoint)
            if (!x.Occupied)
                wp = x;

        return wp;
    }
    

    //GIZMOS STUFF
    Color nodeDefault = Color.white;
    Color nodeOccupied = Color.red;
    private void OnDrawGizmos()
    { 
        Gizmos.color = Occupied ? nodeOccupied : nodeDefault;
        Gizmos.DrawWireSphere(gameObject.transform.position, 0.5f);
        Gizmos.color = Color.green;

        if(nextWaypoint.Count > 0)
            foreach (Waypoint wp in nextWaypoint)
                Gizmos.DrawLine(gameObject.transform.position, wp.gameObject.transform.position);

        Gizmos.color = Color.blue;
        if (barSpot)
            Gizmos.DrawWireCube(gameObject.transform.position, Vector3.one);
            
    }
}
