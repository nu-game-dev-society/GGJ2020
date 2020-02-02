using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Notification : MonoBehaviour
{
    public string text;

    [SerializeField]
    private TextMeshProUGUI textObject;

    [SerializeField]
    private float ttl = 2.0f;

    private float creation = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        creation = Time.time;
        textObject.text = text;

        RectTransform transform = GetComponent<RectTransform>();
        Vector2 anch = transform.anchoredPosition;
        anch.x = 0;
        transform.anchoredPosition = anch;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Time.time - creation) >= ttl)
        {
            Destroy(gameObject);
        }
    }
}
