using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourcePooled : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;



    public void Play(AudioClip clip, float vol = 1f)
    {
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.clip = clip;
        audioSource.volume = vol;
        audioSource.Play();

        StartCoroutine(DelayedReturnToPool());
    }

    IEnumerator DelayedReturnToPool()
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        AudioPool.instance.ReturnToPool(this);
    }
}
