using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    #region Singleton Pattern
    public static HUDManager Instance { get; private set; }
    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    #endregion

    [SerializeField]
    private GameObject useDisplay;

    private void Start()
    {
        useDisplay.SetActive(false);
    }

    public void SetUseDisplay(float total, float current)
    {
        useDisplay.transform.Find("Progress").GetComponent<Image>().fillAmount = current / total;
    }
}
