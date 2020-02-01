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

    List<Complaint> complaints = new List<Complaint>();

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
    void AddComplaint(Complaint complaint)
    {
        if (!complaints.Contains(complaint))
            complaints.Add(complaint);
        else
            Debug.Log("Complaint already active!");
    }

    //Player fixed something
    void RemoveComplaint(Complaint complaint)
    {
        if (!complaints.Contains(complaint))
            complaints.Remove(complaint);
        else
            Debug.Log("Complaint not active!");
    }

    //Tell a new customer about the complaint(s)
    void AssignComplaint()
    {
        List<CustomerController> customers = CustomerManager.Instance.GetAllCustomers();
        foreach (Complaint complaint in complaints)
        {
            foreach(CustomerController customer in customers)
            {
                //customer.ComplainAbout(complaint)
            }
        }
    }
}

//temp
public class Complaint : MonoBehaviour
{

}
