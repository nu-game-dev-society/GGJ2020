using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooSplatter : MonoBehaviour
{
    [SerializeField] List<Sprite> stages;
    [Range(5,0)]
    [SerializeField] int stage;
    float lifetime;

    // Start is called before the first frame update
    void Start()
    {
        lifetime = stage;
        UpdateStage(stage);
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;

        for(int i = 0; i < 5; i++)
        {
            if (lifetime < i) UpdateStage(i);
        }
        
    }

    void UpdateStage(int stage)
    {
        this.stage = stage;
    }
}
