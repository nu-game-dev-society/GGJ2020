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
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Use()
    {
        Debug.Log("Used " + name + " (" + id  + ")");
    }
    public void PickedUp(PlayerController pc)
    {
        pickedUpByPlayer = pc;
        pickupTarget = pc.handTransform;
        rb.useGravity = false;
        Physics.IgnoreCollision(GetComponent<Collider>(), pc.GetComponent<Collider>());
    }
    public void Dropped()
    {
        pickupTarget = null;
        rb.useGravity = true;
    }
    private void FixedUpdate()
    {
        if(pickupTarget)
        {
            Vector3 dir = pickupTarget.position - transform.position;
            float dist = Vector3.Distance(pickupTarget.position, transform.position) * 300f;
            float fwdBoost = 0;
            if (pickedUpByPlayer.inputs.v > 0)
                fwdBoost = pickedUpByPlayer.inputs.v * pickedUpByPlayer.speed * 3f;
            rb.AddForce(((dir * dist) + (fwdBoost * pickedUpByPlayer.transform.forward)) * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }
}

public enum ItemType
{
    Tool,
    Glass,
    Drink,
    Other
}