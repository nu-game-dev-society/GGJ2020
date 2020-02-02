using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RepairPart : MonoBehaviour
{
    [SerializeField]
    private bool isBroken = false;
    public string requiredTool;
    public bool IsBroken
    {
        get { return isBroken; }
        set
        {
            isBroken = value;
            if (isBroken)
            {
                brokenTime = Time.time;
                broken.Invoke();
            }
            else
            {
                repaired.Invoke();
                if (requiredTool != "" && currentTool)
                {
                    currentTool.SetActive(true);
                }
            }
        }
    }
    public UnityEvent startRepairing = new UnityEvent();
    public UnityEvent stopRepairing = new UnityEvent();
    public UnityEvent repaired = new UnityEvent();
    public UnityEvent broken = new UnityEvent();

    private bool repairing = false;
    private float startTime = 0.0f;

    [SerializeField]
    private int repairTime = 1;

    public float breakChance = 0.2f;

    private float brokenTime = 0.0f;

    [SerializeField]
    private float autoRepairTime = 20.0f;

    [SerializeField]
    private int autoRepairCost = 20;

    // Start is called before the first frame update
    void Start()
    {
        IsBroken = isBroken;
    }

    GameObject currentTool;
    public void StartRepair(PlayerController pc)
    {
        if (!repairing && (requiredTool == "" || (pc.heldItem && pc.heldItem.id.Equals(requiredTool))))
        {
            repairing = true;
            startRepairing.Invoke();
            if (requiredTool != "")
            {
                currentTool = pc.heldItem.gameObject;
                currentTool.SetActive(false);
            }
            startTime = Time.time;
        }
    }
    [ContextMenu("ToggleBroken")]
    public void ToggleBroken()
    {
        IsBroken = !isBroken;
    }
    public void StopRepair()
    {
        repairing = false;
        stopRepairing.Invoke();
        if (requiredTool != "" && currentTool)
        {
            currentTool.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsBroken)
        {
            if (repairing)
            {
                float curTime = (Time.time - startTime);
                InteractionController.SetUseDisplay(repairTime, curTime);

                if (curTime >= repairTime)
                {
                    IsBroken = false;
                    repairing = false;
                    InteractionController.FinishRepair();
                }
            }
            else if ((Time.time - brokenTime) >= autoRepairTime)
            {
                RepairManager.Instance.ShowRepairNotification("Auto repaired " + gameObject.name + " for " + MoneySystem.FormatMoney(autoRepairCost));

                IsBroken = false;
                InteractionController.FinishRepair();
                MoneySystem.TakeMoney(autoRepairCost);
            }
        }
    }
}
