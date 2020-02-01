using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletFix : MonoBehaviour
{
    [SerializeField] GameObject poo;
    public void Broken()
    {
        poo.SetActive(true);
    }
    public void Fixed()
    {
        poo.SetActive(false);
    }
}
