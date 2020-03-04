using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Photon.Pun;

public class HUD : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI moneyText;

    [SerializeField]
    private GameObject pausePanel;

    [Header("Clock")]
    [SerializeField]
    private RectTransform bigHand;

    [SerializeField]
    private RectTransform smallHand;

    private PlayerController playerController;
    private InputHandler inputHandler;

    private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        inputHandler = playerController.gameObject.GetComponent<InputHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = MoneySystem.FormatMoney(MoneySystem.GetMoney());

        if (Input.GetButtonDown("Pause"))
        {
            TogglePause();
        }

        DateTime time = DateTime.Now;
        smallHand.rotation = Quaternion.Euler(0, 0, 360f - (0.5f * (60 * time.Hour + time.Minute)));
        bigHand.rotation = Quaternion.Euler(0, 0, 360f - (6f * time.Minute));
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        pausePanel.SetActive(isPaused);

        inputHandler.enabled = !isPaused;
        playerController.ClearInputs();

        if (!PhotonNetwork.IsConnected)
        {
            Time.timeScale = Convert.ToInt32(!isPaused);
        }
        Cursor.visible = isPaused;

        if (isPaused)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;
    }

    public void Exit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
