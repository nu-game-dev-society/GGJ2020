using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

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
    public LayerMask floor;
    public GameObject mesh;
    [HideInInspector] public InventoryItem heldItem;
    public float speed;
    public float mouseSmooth;
    public float lookSens;
    public float crouchDist = 0.45f;
    CapsuleCollider playerCollider;

    public Transform cam;
    Vector2 lookDir;
    Rigidbody rb;
    InteractionController interactionController;
    public Transform handTransform;
    public PhotonView photonView;

    // Start is called before the first frame update
    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        if (photonView)
        {
            if (!photonView.IsMine)
            {
                GetComponent<InputHandler>().enabled = false;
                GetComponentInChildren<Camera>().enabled = false;
                GetComponentInChildren<Rigidbody>().isKinematic = true;
                GetComponentInChildren<AudioListener>().enabled = false;
                mesh.SetActive(true);
            }
        }
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
        if (inputs.crouch)
        {
            ToggleCrouch();
        }
        if (inputs.v != 0 || inputs.h != 0)
            rb.drag = 1;
        else
            rb.drag = 10;

    }

    bool crouching;
    private void ToggleCrouch()
    {
        crouching = !crouching;
        if (crouching)
        {
            StartCoroutine(LerpCameraPosition(cam.localPosition - new Vector3(0, crouchDist, 0)));
            playerCollider.height -= crouchDist;
        }
        else
        {
            StartCoroutine(LerpCameraPosition(cam.localPosition + new Vector3(0, crouchDist, 0)));
            playerCollider.height += crouchDist;
        }
        playerCollider.center = new Vector3(0, playerCollider.height / 2, 0);
    }

    IEnumerator LerpCameraPosition(Vector3 targetPosition)
    {
        float t = 0;
        Vector3 startPos = cam.localPosition;
        while (t < 1)
        {
            t += Time.deltaTime * 3f;
            cam.localPosition = Vector3.Lerp(startPos, targetPosition, t);
            yield return null;
        }
        cam.localPosition = targetPosition;
    }

    void FixedUpdate()
    {
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;
        /*if (Physics.Raycast(transform.position + Vector3.up, Vector3.down * 2f, out RaycastHit hit, floor))
        {
            
            forward = Vector3.ProjectOnPlane(transform.forward, hit.normal);
            right = Vector3.ProjectOnPlane(transform.right, hit.normal);
            forward *= 20f;
            right *= 20f;
        }
        Debug.Log(forward + " " + rb.velocity.magnitude);*/

        Vector3 movement = (inputs.v * forward + inputs.h * right).normalized;
        rb.AddForce(movement * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }
    private void LateUpdate()
    {
        LookCamera();
    }

    public void ClearInputs()
    {
        inputs.v = 0;
        inputs.h = 0;
        inputs.mouseX = 0;
        inputs.mouseY = 0;
        inputs.interactPressed = false;
        inputs.interactReleased = false;
        inputs.crouch = false;
    }
}
