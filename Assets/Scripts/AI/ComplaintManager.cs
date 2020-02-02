using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComplaintManager : MonoBehaviour
{
    #region Singleton Pattern
    public static ComplaintManager Instance { get; private set; }
    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    #endregion
    
    [SerializeField] Material baseMaterial;
    [SerializeField] Sprite defaultImage;
    //[SerializeField] List<Complaint> possibleComplaints = new List<Complaint>();
    
    List<Complaint> activeComplaints = new List<Complaint>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    float cooldown = 10.0f;
    // Update is called once per frame
    void Update()
    {
        if (cooldown > 0.0f)
            cooldown -= Time.deltaTime;
        else
            AssignComplaint();
    }

    //A new thing has gone wrong
    public void Activate(Complaint complaint)
    {
        if (!activeComplaints.Contains(complaint))
            activeComplaints.Add(complaint);
        else
            Debug.Log("Complaint already active!");
    }

    //Player fixed something
    public void Deactivate(Complaint complaint)
    {
        if (activeComplaints.Contains(complaint))
            activeComplaints.Remove(complaint);
        else
            Debug.Log("Complaint not active!");

        //Remove all customers with  this complaint
        List<CustomerController> customers = CustomerSpawner.activeCustomers;
        foreach (CustomerController customer in customers)
        {
            if(customer.complaint)
                if (customer.complaint.id == complaint.id)
                    customer.SetComplaint(null);
        }            
    }

    //Tell a new customer about the complaint(s)
    void AssignComplaint()
    {
        List<CustomerController> customers = CustomerSpawner.activeCustomers;
        foreach (Complaint complaint in activeComplaints)
            customers[Random.Range(0,customers.Count)].SetComplaint(complaint);

        cooldown = 10.0f;
    }
}