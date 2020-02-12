using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalNetworkManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.Instantiate("NetworkedPlayer", transform.position, transform.rotation); //GameObject go = 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
