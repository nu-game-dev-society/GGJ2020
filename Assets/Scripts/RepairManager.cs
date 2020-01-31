using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairManager : MonoBehaviour
{
    private RepairPart[] parts;

    [SerializeField]
    private float breakInterval = 10;

    private float lastBreak;

    // Start is called before the first frame update
    void Start()
    {
        parts = FindObjectsOfType<RepairPart>();
        lastBreak = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Time.time - lastBreak) >= breakInterval)
        {
            lastBreak = Time.time;

            foreach (RepairPart part in parts)
            {
                if (Random.Range(0.0f, 1.0f) <= part.breakChance)
                {
                    Debug.Log("Broken " + part.transform.name);
                    part.IsBroken = true;
                }
            }
        }
    }
}
