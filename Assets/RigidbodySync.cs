using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(PhotonView))]
[RequireComponent(typeof(Rigidbody))]
public class RigidbodySync : MonoBehaviour, IPunObservable
{
    PhotonView pV;
    Rigidbody rb;
    Vector3 networkPosition;
    Quaternion networkRotation;
    bool moved = true;
    // Start is called before the first frame update
    void Awake()
    {
        pV = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody>();
        networkPosition = new Vector3();
        networkRotation = new Quaternion();
    }

    // Update is called once per frame
    void Update()
    {
        if (pV.IsMine)
        {
            if (rb.velocity.sqrMagnitude > 0.01f)
                moved = false;
        }
    }
    public void FixedUpdate()
    {
        if (!pV.IsMine)
        {
            //rb.position = Vector3.MoveTowards(rb.position, networkPosition, this.m_Distance * (1.0f / PhotonNetwork.SerializationRate));
            //rb.rotation = Quaternion.RotateTowards(rb.rotation, networkRotation, this.m_Angle * (1.0f / PhotonNetwork.SerializationRate));
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(moved);
            if (moved)
            {
                stream.SendNext(this.rb.position);
                stream.SendNext(this.rb.rotation);


                stream.SendNext(this.rb.velocity);


            }
            moved = false;
        }



        else
        {
            moved = (bool)stream.ReceiveNext();
        }

    }
}
