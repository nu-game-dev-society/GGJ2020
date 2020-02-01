using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundObject : MonoBehaviour
{
    [SerializeField] Rigidbody m_rigidbody;

    public AudioClip hitClipSoft;
    public AudioClip hitClipHard;
    public AudioClip breakClip;

    bool firstHit = true;

    public Rigidbody[] breakFragments; 

    private void Start()
    {
        m_rigidbody = this.GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (m_rigidbody.velocity.magnitude < 2 && m_rigidbody.velocity.magnitude > 0.5f)
        {

           

            if (Time.time > 0.5f)
                AudioPool.instance.RequestAudioSource(transform.position).Play(hitClipSoft);
        }
        else if (m_rigidbody.velocity.magnitude < 3f)
        {
            if (Time.time > 0.5f)
                AudioPool.instance.RequestAudioSource(transform.position).Play(hitClipHard);
            firstHit = false;
        }
        else
        {
            if (Time.time > 0.5f && breakFragments.Length > 0)
            {
                AudioPool.instance.RequestAudioSource(transform.position).Play(breakClip);

                foreach(Rigidbody frag in breakFragments)
                {
                    frag.gameObject.SetActive(true);
                    frag.transform.SetParent(null);
                    frag.velocity = m_rigidbody.velocity;
                }

                gameObject.SetActive(false);
            }
        }
    }
}

