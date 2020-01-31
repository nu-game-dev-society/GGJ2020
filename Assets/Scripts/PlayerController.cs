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
}
public class PlayerController : MonoBehaviour
{
    public Inputs inputs;
    public InventoryItem heldItem;
    public float speed;
    public float mouseSmooth;
    public float lookSens;
    
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
            interactionController.UnInteract();

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
