using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public PlayerController player;

    void Update()
    {
        player.inputs.v = Input.GetAxis("Vertical");
        player.inputs.h = Input.GetAxis("Horizontal");
        player.inputs.mouseX = Input.GetAxis("Mouse X");
        player.inputs.mouseY = Input.GetAxis("Mouse Y");
        player.inputs.interactPressed = Input.GetButtonDown("Interact");
    }
}
