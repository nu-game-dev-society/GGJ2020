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

    public static GameObject statInteractionUI;
    public static GameObject statRepairUI;
    private static Image repairProgress;
    #endregion

    void Start()
    {
        if (player.photonView && !player.photonView.IsMine)
            Destroy(this);
        statRepairUI = GameObject.Find("Canvas/RepairUI");
        statInteractionUI = GameObject.Find("Canvas/InteractionUI");

        pickupText = statInteractionUI.GetComponentInChildren<TextMeshProUGUI>();
        repairProgress = statRepairUI.transform.Find("Progress").GetComponent<Image>();

        statRepairUI.SetActive(false);
        statInteractionUI.SetActive(false);

    }

    public void Interact()
    {
        if (currentRepair && currentRepair.IsBroken && (currentRepair.requiredTool == "" || (player.heldItem && player.heldItem.id.Equals(currentRepair.requiredTool))))
        {
            statRepairUI.SetActive(true);
            currentRepair.StartRepair(player);
        }
        else if (player.heldItem)
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

    }

    public void ReleaseInteract()
    {
        if (currentRepair)
        {
            statRepairUI.SetActive(false);
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
                statInteractionUI.SetActive(true);
                pickupText.SetText("E to Pickup " + currentItem.itemName);
                break;
            case "Interaction":
                currentInteraction = other.transform.GetComponent<Interaction>();
                statInteractionUI.SetActive(true);
                pickupText.SetText("E to " + currentInteraction.interactionName);
                break;
            case "Repairable":
                currentRepair = other.transform.GetComponent<RepairPart>();
                if (currentRepair.IsBroken)
                {
                    statInteractionUI.SetActive(true);

                    if (currentRepair.requiredTool == "" || (player.heldItem && player.heldItem.id.Equals(currentRepair.requiredTool)))
                        pickupText.SetText("Hold E to Repair");
                    else
                        pickupText.SetText("Find a " + currentRepair.requiredTool);
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
                    statInteractionUI.SetActive(false);
                    currentItem = null;
                }
                break;
            case "Interaction":
                Interaction interaction = other.GetComponent<Interaction>();
                if (interaction == currentInteraction)
                {
                    currentInteraction = null;
                    statInteractionUI.SetActive(false);
                }
                break;
            case "Repairable":
                RepairPart part = other.GetComponent<RepairPart>();
                if (part == currentRepair)
                {
                    currentRepair.StopRepair();
                    currentRepair = null;
                    statInteractionUI.SetActive(false);
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
