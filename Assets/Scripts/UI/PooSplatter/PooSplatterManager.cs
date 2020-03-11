using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooSplatterManager : MonoBehaviour
{
    [SerializeField] GameObject[] prefabs;
    
    GameObject[] cache = new GameObject[50];

    Vector2 canvasSize;

    // Start is called before the first frame update
    void Start()
    {
        FindCanvasSize();
        ValidatePrefabs();
        FillCache();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
            ActivateSplatters(5);
    }

    //NOTE: If canvas size changes during runtime, this will need to be called again (not likely)
    void FindCanvasSize()
    {
        canvasSize = GetComponentInParent<Canvas>().GetComponent<RectTransform>().rect.size;
    }

    //Ensures that the prefabs have been set in the editor
    void ValidatePrefabs()
    {
        if (prefabs.Length == 0)
            Debug.LogError("ERROR: Nobody serialised the fucking poo splatters");
    }
    
    //Pull a splatter from the cache and display it
    void ActivateSplatter()
    {
        try
        {
            GameObject splatter = RequestSplatter();
            splatter.SetActive(true);
            splatter.transform.localPosition += RandomVector(canvasSize.x / 2, canvasSize.y / 2);
        }
        catch(System.NullReferenceException nre)
        {
            Debug.Log("POO REQUEST FAILED!\n" + nre.StackTrace);
        }      
    }
    public void ActivateSplatters(int count = 1)
    {
        for (int i = 0; i < count; i++)
            ActivateSplatter();
    }

    Vector3 RandomVector(float xRange = 0f, float yRange = 0f, float zRange = 0f)
    {
        return new Vector3()
        {
            x = Random.Range(-xRange, xRange),
            y = Random.Range(-yRange, yRange),
            z = Random.Range(-zRange, zRange)
        };
    }

    //Instantiate poo splatters, store in pool
    void FillCache()
    {
        for (int i = 0; i < cache.Length; i++)
            cache[i] = CreateSplatter();
    }

    //Create a new splatter
    GameObject CreateSplatter()
    {
        GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];
        GameObject splatter = Instantiate(prefab, transform);
        splatter.GetComponent<PooSplatter>().Register(this);
        splatter.SetActive(false);
        return splatter;
    }

    //Request an inactive splatter from the pool
    GameObject RequestSplatter()
    {
        //NOTE: This loop will be inefficient if cache is too big
        foreach (GameObject splatter in cache)
            if (!splatter.activeInHierarchy)
                return splatter;
        //If code reaches this point, all cached splatters are currently active
        return null;
    }

    //Function is public to allow poosplatters to return themselves upon expiration
    public void ReturnSplatter(GameObject splatter)
    {
        splatter.transform.localPosition = Vector3.zero;
        splatter.GetComponent<PooSplatter>().ResetFade();
        splatter.SetActive(false);

        if (!System.Array.Exists(cache, s => s == splatter))
            Debug.LogWarning("WARNING: Rogue poos, beware!\nReturned splatter does not belong to cache");
    }
}