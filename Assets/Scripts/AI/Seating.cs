using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Seat
{
    public bool occupied = false;
    public Transform t;
}
public class Seating : MonoBehaviour
{
    
    public Seat[] seats;

    
}
