using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractionController : MonoBehaviour
{
    public PlayerController player;

    public InventoryItem currentItem;
    public RepairPart currentRepair;
    public Interaction currentInteraction;

    #region UI
    public TextMeshProUGUI pickupText;
    public GameObject interactionUI;
    public GameObject repairUI;

    public static GameObject statInteractionUI;
    public static GameObject statRepairUI;
    private static Image repairProgress;
    #endregion

    void Start()
    {
        repairProgress = repairUI.transform.Find("Progress").GetComponent<Image>();
        statInteractionUI = interactionUI;
        statRepairUI = repairUI;
    }

    public void Interact()
    {
        if (player.heldItem)
        {
            player.heldItem.Dropped();
            player.heldItem = null;
        }
        else if (currentItem)
        {
            currentItem.PickedUp(player);
            player.heldItem = currentItem;
        }
        else if (currentInteraction)
        {
            currentInteraction.Interact();
        }
        else if (currentRepair)
        {
            repairUI.SetActive(true);
            currentRepair.StartRepair();
        }
    }

    public void ReleaseInteract()
    {
        if (currentRepair)
        {
            repairUI.SetActive(false);
            currentRepair.StopRepair();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "PickupItem":

                currentItem = other.transform.GetComponent<InventoryItem>();
                if (currentItem.pickupTarget)
                    return;
                interactionUI.SetActive(true);
                pickupText.SetText("E to Pickup " + currentItem.itemName);
                break;
            case "Interaction":
                currentInteraction = other.transform.GetComponent<Interaction>();
                interactionUI.SetActive(true);
                pickupText.SetText("E to " + currentInteraction.interactionName);
                break;
            case "Repairable":
                currentRepair = other.transform.GetComponent<RepairPart>();
                if (currentRepair.IsBroken)
                {
                    interactionUI.SetActive(true);
                    pickupText.SetText("Hold E to Repair");
                }
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
                    interactionUI.SetActive(false);
                    currentItem = null;
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
            case "Repairable":
                RepairPart part = other.GetComponent<RepairPart>();
                if (part == currentRepair)
                {
                    currentRepair.StopRepair();
                    currentRepair = null;
                    interactionUI.SetActive(false);
                }
                break;
        }
    }

    public static void SetUseDisplay(float total, float current)
    {
        repairProgress.fillAmount = current / total;
    }

    public static void FinishRepair()
    {
        statInteractionUI.SetActive(false);
        statRepairUI.SetActive(false);
    }
}
