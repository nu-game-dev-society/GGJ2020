using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio; 

public class AudioPool : MonoBehaviour
{
    public static AudioPool instance;

    public List<AudioSourcePooled> audioSources = new List<AudioSourcePooled>();

    public GameObject audioSourcePrefab;

    public AudioMixer mixer;
    public AudioMixerSnapshot main;
    public AudioMixerSnapshot bathroom;


    public void SnapMixerToMain()
    {
        Debug.LogError("Transition to main");
        main.TransitionTo(0.5f);
    }

    public void SnapMixerToBathroom()
    {
        bathroom.TransitionTo(0.5f);
    }

    private void Awake()
    {
        if (!instance)
            instance = this; 
    }





    private void Start()
    {
        //Populate pool
        PopulatePool(20);

    }

    public void PopulatePool(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject pooledObject =  Instantiate(audioSourcePrefab, transform);
            audioSources.Add(pooledObject.GetComponent<AudioSourcePooled>());
            pooledObject.SetActive(false);
        }
    }

    public AudioSourcePooled RequestAudioSource(Vector3 position)
    {
        AudioSourcePooled source = null;

        if (audioSources.Count > 0)
        {
            source = audioSources[0];
            audioSources.RemoveAt(0);

            source.gameObject.SetActive(true);
            source.transform.position = position;
        }
        else
        {
            GameObject pooledObject = Instantiate(audioSourcePrefab, transform);
            source = pooledObject.GetComponent<AudioSourcePooled>();
            pooledObject.SetActive(true);
            pooledObject.transform.position = position; 
        }

        return source; 
    }


    public void ReturnToPool(AudioSourcePooled source)
    {
        audioSources.Add(source);
        source.gameObject.SetActive(false);
    }
}
