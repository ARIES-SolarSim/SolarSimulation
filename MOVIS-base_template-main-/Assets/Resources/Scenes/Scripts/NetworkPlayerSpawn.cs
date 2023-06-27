using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkPlayerSpawn : MonoBehaviourPunCallbacks
{
    private GameObject spawnedPlayerPrefab;
    private string type;
    

    private void Start()
    {
        

        if (LobbyManager.userType == true) // if it is viewFinder user, instantiate ViewFinderCamera; Trakers will be instantitated in network by headset user.
        {
            type = "camera";
            StartCoroutine(InstantiateViewFinderCamerAfterFewSeconds());
        }
        
        else //if it is headset user, instnatiate trackers for the viewfinder user;
        {
            type = "";
            StartCoroutine(InstantiateTrackerAfterFewSeconds());
            StartCoroutine(InstantiateHeadsetAfterFewSeconds()); // headset user will be instantiated to visualize headset user movement
        }
        
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        
    }

    public override void OnLeftRoom()
    {
        Debug.Log("called");
        base.OnLeftRoom();
        Debug.Log("called");

        if (type == "camera")
        {
            Debug.Log("called");
            //spawnedPlayerPrefab.GetComponent<CameraSetup>().setCanvasInactive();
        }

        PhotonNetwork.Destroy(spawnedPlayerPrefab);
    }

    IEnumerator InstantiateTrackerAfterFewSeconds()
    {
        yield return new WaitForSeconds(2f);
        type = "tracker";
        if (GameObject.Find("Tracker1(Clone)") == null)
        {
        spawnedPlayerPrefab = PhotonNetwork.Instantiate("Tracker1", transform.position, transform.rotation);
        spawnedPlayerPrefab = PhotonNetwork.Instantiate("Tracker2", transform.position, transform.rotation);
        spawnedPlayerPrefab = PhotonNetwork.Instantiate("Tracker3", transform.position, transform.rotation);
        spawnedPlayerPrefab = PhotonNetwork.Instantiate("Tracker4", transform.position, transform.rotation);
        spawnedPlayerPrefab = PhotonNetwork.Instantiate("Tracker5", transform.position, transform.rotation);
        spawnedPlayerPrefab = PhotonNetwork.Instantiate("Tracker6", transform.position, transform.rotation);
        spawnedPlayerPrefab = PhotonNetwork.Instantiate("Tracker7", transform.position, transform.rotation);
        spawnedPlayerPrefab = PhotonNetwork.Instantiate("Tracker8", transform.position, transform.rotation);
        spawnedPlayerPrefab = PhotonNetwork.Instantiate("Tracker9", transform.position, transform.rotation);
    }
    }

    IEnumerator InstantiateViewFinderCamerAfterFewSeconds()
    {
        yield return new WaitForSeconds(2f);
        type = "camera";
        spawnedPlayerPrefab = PhotonNetwork.Instantiate("ViewFinderCamera", transform.position, transform.rotation);
        //spawnedPlayerPrefab.transform.SetParent(GameObject.Find("[CameraRig]").transform);
    }

    IEnumerator InstantiateHeadsetAfterFewSeconds()
    {
        yield return new WaitForSeconds(2.5f);
        type = "headset";
        if (GameObject.Find("VR Headset Network Player") == null)
        spawnedPlayerPrefab = PhotonNetwork.Instantiate("HeadsetUser", transform.position, transform.rotation);
        //spawnedPlayerPrefab.transform.SetParent(GameObject.Find("[CameraRig]").transform);
    }
}
