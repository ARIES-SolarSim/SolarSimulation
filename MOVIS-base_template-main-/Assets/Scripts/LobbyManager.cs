using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    [Tooltip("true is viewFinder, false is headset")]
    public static bool userType;

    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    public void ClickedHeadset()
    {
        userType = false; //set as Headset user

    }
    public void ClickedViewFinder()
    {
        Debug.Log("clicked View finder");
        userType = true; //set as ViewFinder user
    }

    public void ClickedDocent()
    {
        userType = true;
    }

    public void ClickedEnterRoom()
    {
        PhotonNetwork.LoadLevel(1); //load the Scene 1 (Room1)
    }
}
