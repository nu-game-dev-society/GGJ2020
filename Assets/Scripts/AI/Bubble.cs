using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] SpriteRenderer bubble;
    [SerializeField] MeshRenderer left;
    [SerializeField] MeshRenderer middle;
    [SerializeField] MeshRenderer right;

    private void Awake()
    {
        //Default to empty
        Clear();
    }

    public void Draw(List<Material> icons)
    {
        //Redundant but defensive
        //At this point, we know bubble is disabled & ready for new icons
        Clear();

        //Draw differently depending on number of items
        switch (icons.Count)
        {
            case 1: //Draw in middle
                SetMiddle(icons[0]);
                break;
            case 2: //Draw either side
                SetLeft(icons[0]);
                SetRight(icons[1]);
                break;
            case 3: //Draw in all 3 spots
                SetLeft(icons[0]);
                SetMiddle(icons[1]);
                SetRight(icons[2]);
                break;
            default:
                Debug.Log("Too many/few items in bubble");
                break;
        }

        //Enable bubble
        bubble.enabled = true;
    }


    void SetIcon(MeshRenderer slot, Material material)
    {
        slot.material = material;
        slot.enabled = slot.material != null; //If material is null, disable renderer
    }
    void SetLeft(Material material) => SetIcon(left, material);
    void SetMiddle(Material material) => SetIcon(middle, material);
    void SetRight(Material material) => SetIcon(right, material);


    public void Clear()
    {
        foreach (Renderer renderer in new Renderer[] { left, middle, right, bubble })
            renderer.enabled = false;
    }
}
