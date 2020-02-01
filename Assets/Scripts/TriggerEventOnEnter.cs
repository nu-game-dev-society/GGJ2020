using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class TriggerEventOnEnter : MonoBehaviour
{

    [SerializeField] UnityEvent OnEnterTrigger;
    [SerializeField] UnityEvent OnExitTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            Debug.Log("Aye");
            OnEnterTrigger.Invoke();
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 11)
            OnExitTrigger.Invoke();
    }
}
