﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class ServerManager : MonoBehaviourPunCallbacks
{
    [SerializeField] TextMeshProUGUI serverStatusUI;
    [SerializeField] GameObject startGameButton;
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void MultiplayerButtonPressed()
    {
        Debug.Log("Started");
        serverStatusUI.SetText("Connecting");
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        serverStatusUI.SetText("Joining Room");
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        int randomRoomName = Random.Range(0, 100000000);
        RoomOptions ro = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 2 };
        PhotonNetwork.CreateRoom("Room: " + randomRoomName, ro);
        
        startGameButton.SetActive(true);

    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        //PhotonNetwork.AutomaticallySyncScene = true;
        serverStatusUI.SetText("Room: " + PhotonNetwork.CurrentRoom.Name);
    }
    public void Startgame()
    {
        
        PhotonNetwork.LoadLevel("MultiplayerScene");
        //photonView.RPC("LoadScene", RpcTarget.All);

    }

    [PunRPC]
    void LoadScene()
    {
        SceneManager.LoadScene("MultiplayerScene");
    }
    
    void Update()
    {
        
    }
}
