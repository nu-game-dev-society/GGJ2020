using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairPart : MonoBehaviour
{
    [SerializeField]
    private bool isBroken = true;

    public bool IsBroken
    {
        get { return isBroken; }
        set { isBroken = value; repairModel.SetActive(isBroken); }
    }

    [SerializeField]
    private GameObject repairModel;

    private bool repairing = false;
    private float startTime = 0;

    [SerializeField]
    private int repairTime = 1;

    public float breakChance = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        IsBroken = isBroken;
    }

    public void StartRepair()
    {
        if (!repairing)
        {
            repairing = true;
            startTime = Time.time;
        }
    }

    public void StopRepair()
    {
        repairing = false;
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
                }
            }
        }
    }
}
