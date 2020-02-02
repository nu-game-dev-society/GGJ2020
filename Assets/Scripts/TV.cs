using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV : MonoBehaviour
{


    public Renderer rend;
    public Material mat;

    public Material[] images;
   
    void Update()
    {
        if (Time.frameCount % 400 == 0)
            rend.sharedMaterial = images[Random.Range(0, 3)];
    }
}
