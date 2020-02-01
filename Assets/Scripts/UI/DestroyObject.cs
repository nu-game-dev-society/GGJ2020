using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    //Destroyed version of the object
    public GameObject destoyedVersion;


    //call this method to destroy the object on Action
    void destroyObject()
    {
        Instantiate(destoyedVersion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
