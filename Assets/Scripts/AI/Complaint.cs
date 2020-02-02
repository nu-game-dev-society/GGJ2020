using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Complaint : MonoBehaviour
{
    //Need an image to send
    [SerializeField] public string description;
    [SerializeField] public int id;
    [SerializeField] Transform content;
    

    Transform player;
    CustomerController customer;
    private void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
        customer = GetComponentInParent<CustomerController>();

        customer.complaintMat = ComplaintManager.Instance.GetComplaintImageMat(id);

        //Set active if they definitely have a complaint
        content.gameObject.SetActive(customer.complaint != null);
    }

    void FixedUpdate()
    {
        bool problemSolved = false;
        problemSolved = ComplaintManager.Instance.Check(id);

        if (problemSolved)
        {
            customer.serviceComplete = true;
            Destroy(gameObject);
        }
    }

    void Update()
    {
        transform.LookAt(player);
    }
}