using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    public PlayerController player;
    public TextMeshProUGUI pickupText;
    public InventoryItem currentItem;
    public Interaction currentInteraction;
    public GameObject interactionUI;
    void Start()
    {

    }

    public void Interact()
    {
        if (currentItem)
        {
            currentItem.PickedUp(player);
            player.heldItem = currentItem;
        }
        else if (currentInteraction)
        {
            currentInteraction.Interact();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "PickupItem":
                currentItem = other.transform.GetComponent<InventoryItem>();
                interactionUI.SetActive(true);
                pickupText.SetText("E to Pickup " + currentItem.itemName);
                break;
            case "Interaction":
                currentInteraction = other.transform.GetComponent<Interaction>();
                interactionUI.SetActive(true);
                pickupText.SetText("E to " + currentInteraction.interactionName);
                break;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        switch (other.tag)
        {
            case "PickupItem":
                InventoryItem item = other.GetComponent<InventoryItem>();
                if (item == currentItem)
                {
                    if (item.pickupTarget)
                        return;
                    currentItem = null;
                    interactionUI.SetActive(false);
                }
                break;
            case "Interaction":
                Interaction interaction = other.GetComponent<Interaction>();
                if (interaction == currentInteraction)
                {
                    currentInteraction = null;
                    interactionUI.SetActive(false);
                }
                break;
        }
    }
}
