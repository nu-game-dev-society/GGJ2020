using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooSplatter : MonoBehaviour
{
    [Range(5,0)][SerializeField] float lifetime = 5f;

    SpriteRenderer spriteRenderer;
    PooSplatterManager splatterManager;

    // Start is called before the first frame update
    void Start()
    {
        splatterManager = FindObjectOfType<PooSplatterManager>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();        
    }

    public void BeginFade() => StartCoroutine(Fade());
    public void ResetFade() => ChangeAlpha(255f);

    IEnumerator Fade()
    {
        //ChangeAlpha(spriteRenderer.color.a - (255f / lifetime));
        ChangeAlpha(spriteRenderer.color.a - 1f);

        Debug.Log(name + " fading... Alpha: " + spriteRenderer.color.a);

        yield return new WaitForSeconds(.1f);
    }

    void Update()
    {
        if (spriteRenderer.color.a <= 0f)
            splatterManager.ReturnSplatter(gameObject);
    }

    void ChangeAlpha(float newAlpha)
    {
        spriteRenderer.color = new Color()
        {
            r = spriteRenderer.color.r,
            g = spriteRenderer.color.g,
            b = spriteRenderer.color.b,
            a = newAlpha
        };
    }
}
