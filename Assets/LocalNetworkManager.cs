using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalNetworkManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        PhotonNetwork.Instantiate("NetworkedPlayer", transform.position, transform.rotation);
        //StartCoroutine(DelaySpawn());
        //GameObject go = 
    }
    IEnumerator DelaySpawn()
    {
        yield return new WaitForSeconds(4); //WILL CAUSE AWAKE ISSUES
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
