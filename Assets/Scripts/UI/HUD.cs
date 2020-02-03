using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class HUD : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI moneyText;

    [SerializeField]
    private GameObject pausePanel;

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
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        pausePanel.SetActive(isPaused);

        inputHandler.enabled = !isPaused;
        playerController.ClearInputs();

        Time.timeScale = Convert.ToInt32(!isPaused);
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
