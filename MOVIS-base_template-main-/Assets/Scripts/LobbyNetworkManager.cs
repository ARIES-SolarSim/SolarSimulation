 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;
public class LobbyNetworkManager : MonoBehaviourPunCallbacks
{
    public GameObject JoinedRoomPanel;
    public GameObject SplashPagePanel;
    public GameObject image;
    public GameObject splash;
    public Button getStarted;
    public void ClickedHeadsetUser()
    {

        StartCoroutine(EnableJoinedRoomPanelAfterFewSeconds()); //wait for few seconds to connect the server
    }
    public void ClickedDocent()
    {
        PhotonNetwork.NickName = "9";
        StartCoroutine(EnableJoinedRoomPanelAfterFewSeconds());
    }
    public void ClickedOne()
    {
        PhotonNetwork.NickName = "1";
        StartCoroutine(EnableJoinedRoomPanelAfterFewSeconds()); //wait for few seconds to connect the server
        getStarted.gameObject.SetActive(false);

    }
    public void ClickedTwo()
    {
        PhotonNetwork.NickName = "2";
        StartCoroutine(EnableJoinedRoomPanelAfterFewSeconds());
        getStarted.gameObject.SetActive(false);
    }
    public void ClickedThree()
    {
        PhotonNetwork.NickName = "3";
        StartCoroutine(EnableJoinedRoomPanelAfterFewSeconds());
        getStarted.gameObject.SetActive(false);
    }
    public void ClickedFour()
    {
        PhotonNetwork.NickName = "4";
        StartCoroutine(EnableJoinedRoomPanelAfterFewSeconds());
        getStarted.gameObject.SetActive(false);
    }
    public void ClickedFive()
    {
        PhotonNetwork.NickName = "5";
        StartCoroutine(EnableJoinedRoomPanelAfterFewSeconds());
        getStarted.gameObject.SetActive(false);
    }
    public void ClickedSix()
    {
        PhotonNetwork.NickName = "6";
        StartCoroutine(EnableJoinedRoomPanelAfterFewSeconds());
        getStarted.gameObject.SetActive(false);
    }
    public void ClickedSeven()
    {
        PhotonNetwork.NickName = "7";
        StartCoroutine(EnableJoinedRoomPanelAfterFewSeconds());
        getStarted.gameObject.SetActive(false);
    }
    public void ClickedEight()
    {
        PhotonNetwork.NickName = "8";
        StartCoroutine(EnableJoinedRoomPanelAfterFewSeconds());
        getStarted.gameObject.SetActive(false);
    }



    public void ConnectToServer()
    {

        PhotonNetwork.ConnectUsingSettings();

        if (LobbyManager.userType) //if it is true, ViewFinder User
        {
            
            //do nothing
        }
        else //if it is headset user
        {
            
            PhotonNetwork.NickName = "VR Headset Network Player";
        }

        Debug.Log("Try Connect to Server...");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected To Server");
        base.OnConnectedToMaster();

        PhotonNetwork.JoinLobby(); //if the server is connected, automatically joined the lobby
    }
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("Joined Lobby");

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            //do nothing
        }
        else
        {
            PhotonNetwork.LoadLevel(0); // go back to lobby
        }

    }
    public void InitializeRoom() //join or create room1
    {

        Debug.Log("initialize room");
        //Room option
        RoomOptions roomOptions = new RoomOptions()
        {
            MaxPlayers = 10,
            IsVisible = true,
            IsOpen = true,
            PublishUserId = true
        };

        PhotonNetwork.JoinOrCreateRoom("Room1", roomOptions, TypedLobby.Default);
        
    }

    public override void OnJoinedRoom()
    {
        
        Debug.Log("Joined a Room");
        base.OnJoinedRoom();
        for(int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            Debug.Log(i);
        }
      
        
    
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("A new player entered the room");
        if(newPlayer.NickName == "VR Headset Network Player")
        {
            PhotonNetwork.SetMasterClient(newPlayer);
        }
        base.OnPlayerEnteredRoom(newPlayer);
    }

    IEnumerator EnableJoinedRoomPanelAfterFewSeconds()
    {
        yield return new WaitForSeconds(2.5f);
        //yield return null;
        JoinedRoomPanel.SetActive(true);
    }

    public void clickedStart()
    {     
        StartCoroutine(EnableSplashScreenAfterFewSeconds());
    }

    IEnumerator EnableSplashScreenAfterFewSeconds()
    {
        yield return null;
        JoinedRoomPanel.SetActive(false);
        image.SetActive(false);
        splash.SetActive(true);
        SplashPagePanel.SetActive(true);
    }

    public void disableButtton() {
        getStarted.enabled = false;
    }
}
