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
        //If not set in editor, find them manually
        if (bubble == null) bubble = gameObject.GetComponent<SpriteRenderer>();
        if (left == null) left = gameObject.GetComponents<MeshRenderer>()[0];
        if (middle == null) middle = gameObject.GetComponents<MeshRenderer>()[1];
        if (right == null) right = gameObject.GetComponents<MeshRenderer>()[2];

        //Default to empty
        Clear();
    }

    public void Draw(List<Material> materials)
    {
        //Redundant but defensive
        //At this point, we know bubble is disabled & ready for new icons
        Clear();

        //Draw differently depending on number of items
        switch (materials.Count)
        {
            case 1: //Draw in middle
                SetMiddle(materials[0]);
                break;
            case 2: //Draw either side
                SetLeft(materials[0]);
                SetRight(materials[1]);
                break;
            case 3: //Draw in all 3 spots
                SetLeft(materials[0]);
                SetMiddle(materials[1]);
                SetRight(materials[2]);
                break;
            default:
                Debug.Log("Andrew fucked up the new bubble system");
                break;
        }
        //Enable bubble
        bubble.enabled = true;
    }

    void SetIcon(MeshRenderer slot, Material icon)
    {
        slot.material = icon;
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
