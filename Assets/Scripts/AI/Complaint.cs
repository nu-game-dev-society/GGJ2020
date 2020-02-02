using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Complaint : MonoBehaviour
{
    //Need an image to send
    [SerializeField] public string description;
    [SerializeField] public int id;
    

    Transform player;
    CustomerController customer;
    private void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
        //customer = GetComponentInParent<CustomerController>();        
    }

    void FixedUpdate()
    {
        bool problemSolved = false;
//      problemSolved = ComplaintManager.Instance.Check(id);

        if (problemSolved)
        {
            ComplaintManager.Instance.Deactivate(this);
        }
    }

    void Update()
    {
        transform.LookAt(player);
    }
}