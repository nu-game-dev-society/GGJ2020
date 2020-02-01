using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inputs
{
    public float v, h;
    public float mouseX, mouseY;
    public bool interactPressed;
    public bool interactReleased;
    public bool crouch;
}
public class PlayerController : MonoBehaviour
{
    public Inputs inputs;
    [HideInInspector] public InventoryItem heldItem;
    public float speed;
    public float mouseSmooth;
    public float lookSens;
    CapsuleCollider playerCollider;
    
    public Transform cam;
    Vector2 lookDir;
    Rigidbody rb;
    InteractionController interactionController;
    public Transform handTransform;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        interactionController = GetComponentInChildren<InteractionController>();
        playerCollider = GetComponent<CapsuleCollider>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LookCamera()
    {
        Vector2 lookDelta = new Vector2();
        lookDelta.x = Mathf.Lerp(lookDelta.x, inputs.mouseX, mouseSmooth);
        lookDelta.y = Mathf.Lerp(lookDelta.y, inputs.mouseY, mouseSmooth);

        lookDelta *= lookSens;

        lookDir += lookDelta;

        lookDir.y = Mathf.Clamp(lookDir.y, -80, 80);

        cam.localRotation = Quaternion.AngleAxis(-lookDir.y, Vector3.right);
        transform.localRotation = Quaternion.AngleAxis(lookDir.x, Vector3.up);
    }
    private void Update()
    {
        if (inputs.interactPressed)
            interactionController.Interact();

        if (inputs.interactReleased)
            interactionController.ReleaseInteract();
        if(inputs.crouch)
        {
            ToggleCrouch();
        }

    }

    bool crouching;
    private void ToggleCrouch()
    {
        crouching = !crouching;
        if(crouching)
        {
            cam.localPosition -= new Vector3(0, 0.9f, 0);
            playerCollider.height -= 0.9f;
        }
        else
        {
            cam.localPosition += new Vector3(0, 0.9f, 0);
            playerCollider.height += 0.9f;
        }
        playerCollider.center = new Vector3(0, playerCollider.height / 2, 0);
    }

    void FixedUpdate()
    {
        Vector3 movement = (inputs.v * transform.forward + inputs.h * transform.right).normalized;
        rb.AddForce(movement * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }
    private void LateUpdate()
    {
        LookCamera();
        
    }
}
