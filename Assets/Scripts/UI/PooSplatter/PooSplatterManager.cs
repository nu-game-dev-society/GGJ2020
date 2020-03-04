using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooSplatterManager : MonoBehaviour
{
    [SerializeField] List<GameObject> splatterPrefabs;    
    [SerializeField] Transform canvas;

    float pooLevel = 0f;

    List<GameObject> splatterPool;
    const int poolSize = 100;
    
    // Start is called before the first frame update
    void Start()
    {
        if(splatterPrefabs == null || splatterPrefabs.Count == 0)
        {
            splatterPrefabs = new List<GameObject>();
            Debug.LogError("ERROR: Nobody serialised the fucking poo splatters");
        }
        FillPool();
    }

    // Update is called once per frame
    void Update()
    {
        //TEMPORARY DEBUG BUTTON
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            IncreasePooLevel();            
        }

        if (pooLevel > 0.0f) DecreasePooLevel(Time.deltaTime);
        if (pooLevel < 0.0f) ClearPooLevel();
        if (pooLevel > 1.0f) ActivateSplatter();
        //Debug.Log("POO: " + pooLevel);
    }

    void ActivateSplatter()
    {
        GameObject splatter = RequestSplatter();
        if (splatter)
        {
            splatter.SetActive(true);
            splatter.transform.localPosition += RandomVector(Screen.width / 2, Screen.height / 2);
            splatter.GetComponent<PooSplatter>().BeginFade();
            //Generating a new splatter "spends" pooLevel
            DecreasePooLevel(1.0f);
        }
        else
        {
            Debug.Log("POO REQUEST FAILED");
        }        
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

    void IncreasePooLevel(float amount = 1.0f) => pooLevel += amount;
    void DecreasePooLevel(float amount = 1.0f) => pooLevel -= amount;
    void ClearPooLevel() => pooLevel = 0.0f;

    //Instantiate poo splatters, store in pool
    void FillPool()
    {
        splatterPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject prefab = splatterPrefabs[Random.Range(0, splatterPrefabs.Count)];
            GameObject splatter = Instantiate(prefab, transform);
            splatter.SetActive(false);
            splatterPool.Add(splatter);
        }
    }

    //Request an inactive splatter from the pool
    GameObject RequestSplatter()
    {
        //NOTE: This loop will be inefficient if poolsize is too big
        foreach (GameObject splatter in splatterPool)
            if (!splatter.activeInHierarchy)
                return splatter;
        //If code reaches this point, all pooled splatters are currently active
        return null;
    }

    //Function is public to allow poosplatters to return themselves upon expiration
    public void ReturnSplatter(GameObject splatter)
    {
        splatter.transform.localPosition = Vector3.zero;
        splatter.GetComponent<PooSplatter>().ResetFade();
        splatter.SetActive(false);

        if (!splatterPool.Contains(splatter))
            Debug.LogWarning("WARNING: Rogue poos, beware!\nReturned splatter does not belong to splatter pool");
    }
}