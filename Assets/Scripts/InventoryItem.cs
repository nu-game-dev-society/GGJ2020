using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public string id;
    public string itemName;
    public ItemType type;
    public PlayerController pickedUpByPlayer;
    public Transform pickupTarget;
    Rigidbody rb;


    float defaultAngular; 

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        defaultAngular = rb.angularDrag; 
    }

    public void Use()
    {
        Debug.Log("Used " + name + " (" + id + ")");
    }
    public void PickedUp(PlayerController pc)
    {
        pickedUpByPlayer = pc;
        pickupTarget = pc.handTransform;
        rb.angularDrag = 1f;
        //rb.useGravity = false;
        Collider[] cols = GetComponents<Collider>();
        foreach (Collider c in cols)
            Physics.IgnoreCollision(c, pc.GetComponent<Collider>());
    }
    public void Dropped()
    {
        pickupTarget = null;
        pickedUpByPlayer.heldItem = null;
        rb.angularDrag = defaultAngular;
        //rb.useGravity = true;

    }

    private void FixedUpdate()
    {
        if (pickupTarget)
        {
            Vector3 dir = pickupTarget.position - transform.position;
            rb.velocity = dir * 10f;

        }
    }

    public enum ItemType
    {
        Tool,
        Glass,
        Drink,
        Other
    }
}