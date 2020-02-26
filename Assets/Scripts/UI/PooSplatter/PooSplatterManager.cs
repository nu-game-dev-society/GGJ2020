using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooSplatterManager : MonoBehaviour
{

    [SerializeField] List<GameObject> splatterPrefabs;
    float pooLevel = 0f;
    List<PooSplatter> activeSplatters;
    [SerializeField] Transform canvas;

    // Start is called before the first frame update
    void Start()
    {
        if(splatterPrefabs == null)
        {
            splatterPrefabs = new List<GameObject>();
            Debug.LogWarning("WARNING: Nobody serialised the fucking poo splatters");
        }

        activeSplatters = new List<PooSplatter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            IncreasePooLevel();            
        }

        if (pooLevel > 0.0f) DecreasePooLevel(Time.deltaTime);
        if (pooLevel < 0.0f) ClearPooLevel();
        if (pooLevel > 1.0f) GenerateSplatter();
        Debug.Log("POO: " + pooLevel);
    }

    void GenerateSplatter()
    {
        //Generating a new splatter "spends" pooLevel
        DecreasePooLevel(1.0f);

        //Instantiate a new poo splatter, position it randomly on the screen
        GameObject go = Instantiate(splatterPrefabs[0], canvas.transform);

        //go.transform.localPosition += RandomVector();
        go.transform.localPosition += RandomVector(Screen.width/2,Screen.height/2);
        Debug.Log("GENERATED POO");
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
}