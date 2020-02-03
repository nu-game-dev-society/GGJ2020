using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV : MonoBehaviour
{
    [SerializeField]
    private Renderer rend;

    [SerializeField]
    private int framesPerImage = 400;

    [SerializeField]
    private Material[] images;
   
    void Update()
    {
        if (Time.frameCount % framesPerImage == 0)
            rend.sharedMaterial = images[Random.Range(0, 3)];
    }
}
