using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamera : MonoBehaviour
{
    [SerializeField]
    private Transform start;

    [SerializeField]
    private Transform end;

    public float movementTime = 1;
    public float rotationSpeed = 0.1f;

    private Vector3 refPos;
    private Vector3 refRot;

    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = start;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, start.position) <= 0.1f)
            target = end;

        if (Vector3.Distance(transform.position, end.position) <= 0.1f)
            target = start;

        transform.position = Vector3.SmoothDamp(transform.position, target.position, ref refPos, movementTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, rotationSpeed * Time.deltaTime);
    }
}
