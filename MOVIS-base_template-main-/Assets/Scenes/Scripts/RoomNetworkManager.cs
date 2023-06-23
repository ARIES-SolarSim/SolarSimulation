using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomNetworkManager : MonoBehaviourPunCallbacks
{
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined a Room");
        base.OnJoinedRoom();
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("A new player entered the room");
        base.OnPlayerEnteredRoom(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("Player left room");

        if (otherPlayer.NickName == "1")
        {
            GameObject.Find("Canvases").transform.GetChild(0).GetComponent<Canvas>().gameObject.SetActive(false);
        }
        if (otherPlayer.NickName == "2")
        {
            GameObject.Find("Canvases").transform.GetChild(1).GetComponent<Canvas>().gameObject.SetActive(false);
        }
        if (otherPlayer.NickName == "3")
        {
            GameObject.Find("Canvases").transform.GetChild(2).GetComponent<Canvas>().gameObject.SetActive(false);
        }
        if (otherPlayer.NickName == "4")
        {
            GameObject.Find("Canvases").transform.GetChild(3).GetComponent<Canvas>().gameObject.SetActive(false);
        }
        if (otherPlayer.NickName == "5")
        {
            GameObject.Find("Canvases").transform.GetChild(4).GetComponent<Canvas>().gameObject.SetActive(false);
        }
        if (otherPlayer.NickName == "6")
        {
            GameObject.Find("Canvases").transform.GetChild(5).GetComponent<Canvas>().gameObject.SetActive(false);
        }
        if (otherPlayer.NickName == "7")
        {
            GameObject.Find("Canvases").transform.GetChild(6).GetComponent<Canvas>().gameObject.SetActive(false);
        }
        if (otherPlayer.NickName == "8")
        {
            GameObject.Find("Canvases").transform.GetChild(7).GetComponent<Canvas>().gameObject.SetActive(false);
        }
        if (otherPlayer.NickName == "9")
        {
            GameObject.Find("Canvases").transform.GetChild(8).GetComponent<Canvas>().gameObject.SetActive(false);
        }

        base.OnPlayerLeftRoom(otherPlayer);
    }
}
