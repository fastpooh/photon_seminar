using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro; 
using UnityEngine.UI;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    private readonly string version = "1.0";
    private string userId = "Chris";                // 편의상 user ID 고정, 이후 변경해야 함

    // Multiplay Settings
    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = version;
        PhotonNetwork.NickName = userId;
        PhotonNetwork.ConnectUsingSettings();
    }

    // If connected to internet, join lobby
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    // When no room exists and try to enter room, OnJoinRandomFailed is called
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = 2;
        ro.IsOpen = true;
        ro.IsVisible = true;

        PhotonNetwork.CreateRoom("Room1", ro);     // Make new game room
    }

    // When you join a room
    public override void OnJoinedRoom()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("InGame");
        }
    }

#region  UI_BUTTON_EVENT
    // Exit Game
    public void OnExittClick()
    {
        Application.Quit();
    }

    // Enter room
    public void OnStartClick()
    {
        PhotonNetwork.JoinRandomRoom();
    }
#endregion
}