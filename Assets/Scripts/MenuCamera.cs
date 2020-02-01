using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuCamera : MonoBehaviour
{
    [SerializeField]
    Image bgImage;
    [SerializeField]
    Transform cameraLookPoint;
    [SerializeField]
    private Transform[] start;

    [SerializeField]
    private Transform[] end;

    public float movementTime = 1;
    public float rotationSpeed = 0.1f;

    private Vector3 refPos;
    private Vector3 refRot;

    private Transform target;

    // Start is called before the first frame update
    void Start()
    {

    }

    float t = 0;
    int i = 0;
    void Update()
    {
        t += (Time.deltaTime / movementTime);
        transform.position = Vector3.Slerp(start[0].position, end[0].position, t);
        if(t>0.95f)
        {
            bgImage.color = new Color(0, 0, 0, 1-(20 * (1 - t)));
        }
        else if (t <= 0.05f)
        {
            bgImage.color = new Color(0, 0, 0, 1-(20 * t));
        }
        if (t > 1)
        {
            t = 0;
            i++;
            if (i >= start.Length)
                i = 0;
        }
        transform.LookAt(cameraLookPoint);
    }
}
