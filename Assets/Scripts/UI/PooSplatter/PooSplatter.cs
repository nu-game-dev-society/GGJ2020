using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PooSplatter : MonoBehaviour
{
    Image image;
    PooSplatterManager splatterManager;
    const float FADERATE = 0.5f;

    // Start is called before the first frame update
    void Awake()
    {
        image = gameObject.GetComponent<Image>();
    }

    void Update()
    {
        if (image.color.a > 0f)
            Fade();
        else
            splatterManager.ReturnSplatter(gameObject);
    }

    public void Register(PooSplatterManager splatterManager) => this.splatterManager = splatterManager;

    void Fade()
    {
        float newAlpha = Mathf.Lerp(image.color.a, image.color.a - 1f, Time.deltaTime * FADERATE);
        SetAlpha(newAlpha);
    }
    public void ResetFade() => SetAlpha(1f);

    //Leave RGB the same, change alpha value only
    void SetAlpha(float newAlpha)
    {
        image.color = new Color()
        {
            r = image.color.r,
            g = image.color.g,
            b = image.color.b,
            a = newAlpha
        };
    }

    
}
