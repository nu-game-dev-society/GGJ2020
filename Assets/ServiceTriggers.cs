using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceTriggers : MonoBehaviour
{
    public CustomerController cust;
    public InventoryItem curitem;
    // Update is called once per frame
    void Update()
    {
        if(curitem !=null)
        {
            if (curitem.pickupTarget == null)
            {
                if (cust)
                {
                    if (cust.PickupItem(curitem.id))
                    {
                        Destroy(curitem.gameObject);
                        curitem = null;
                    }
                }
            }

        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickupItem"))
            curitem = other.GetComponent<InventoryItem>();
    }
    private void OnTriggerExit(Collider other)
    {
        if (curitem)
        {
            if (other.gameObject == curitem.gameObject)
                curitem = null;
        }
    }
}
