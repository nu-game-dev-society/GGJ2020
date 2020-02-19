using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalNetworkManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelaySpawn());
        //GameObject go = 
    }
    IEnumerator DelaySpawn()
    {
        yield return new WaitForSeconds(4);
        PhotonNetwork.Instantiate("NetworkedPlayer", transform.position, transform.rotation);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
