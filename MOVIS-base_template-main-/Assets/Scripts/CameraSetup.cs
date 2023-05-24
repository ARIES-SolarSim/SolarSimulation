using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
public class CameraSetup : MonoBehaviour
{
    public GameObject Tracker1, Tracker2, Tracker3, Tracker4, Tracker5, Tracker6, Tracker7, Tracker8, Tracker9;
    private Canvas canvas;

    private PhotonView photonView;
    [SerializeField]
    private GameObject[] viewFinderCameras;

    public Quaternion cameraOffset;
    void Start()
    {
        if (photonView == null)
        {
            photonView = GetComponent<PhotonView>();
        }

        if (photonView.IsMine) //revmoe the tag so that myself is not disabled in the update funciton
        {
            Debug.Log("setting tag myself for " + photonView.Owner.NickName);
            this.gameObject.tag = "Myself";
        }

        
        StartCoroutine(FindTrackerAfterFewSeconds()); //give few seconds for the systems to settle
        this.gameObject.name = photonView.Owner.NickName;
        trackerSetup();
        setCanvas();

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Tracker3 == null)
        {
            trackerSetup();
        }

        if (photonView.Owner.NickName == "1")
        {
            MapTrackerPosition(Tracker1);
        }
        else if (photonView.Owner.NickName == "2")
        {
            MapTrackerPosition(Tracker2);
        }
        else if (photonView.Owner.NickName == "3")
        {
            MapTrackerPosition(Tracker3);
        }
        else if (photonView.Owner.NickName == "4")
        {
            MapTrackerPosition(Tracker4);
        }
        else if (photonView.Owner.NickName == "5")
        {
            MapTrackerPosition(Tracker5);
        }
        else if (photonView.Owner.NickName == "6")
        {
            MapTrackerPosition(Tracker6);
        }
        else if (photonView.Owner.NickName == "7")
        {
            MapTrackerPosition(Tracker7);
        }
        else if (photonView.Owner.NickName == "8")
        {
            MapTrackerPosition(Tracker8);
        }
        else if (photonView.Owner.NickName == "9")
        {
            MapTrackerPosition(Tracker9);
        }


        //disable the camera that is not my view
        viewFinderCameras = GameObject.FindGameObjectsWithTag("ViewFinderCamera");
        for (int i = 0; i < viewFinderCameras.Length; i++)
        {
            if (i.ToString() != photonView.Owner.NickName)
            {
                viewFinderCameras[i].SetActive(false);
            }
        }

    }

    /**
     * Sets the canvases of each tablet so it has its own UI
     */
    void setCanvas()
    {
        if (photonView.Owner.NickName == "1")
        {
            GameObject.Find("Canvases").transform.GetChild(0).GetComponent<Canvas>().gameObject.SetActive(true);
            canvas = GameObject.Find("Canvases/Canvas1").GetComponent<Canvas>();
            canvas.worldCamera = this.GetComponent<Camera>();
        }
        else if (photonView.Owner.NickName == "2")
        {
            GameObject.Find("Canvases").transform.GetChild(1).GetComponent<Canvas>().gameObject.SetActive(true);
            canvas = GameObject.Find("Canvases/Canvas2").GetComponent<Canvas>();
            canvas.worldCamera = this.GetComponent<Camera>();
        }
        else if (photonView.Owner.NickName == "3")
        {
            GameObject.Find("Canvases").transform.GetChild(2).GetComponent<Canvas>().gameObject.SetActive(true);
            canvas = GameObject.Find("Canvases/Canvas3").GetComponent<Canvas>();
            canvas.worldCamera = this.GetComponent<Camera>();
        }
        else if (photonView.Owner.NickName == "4")
        {
            GameObject.Find("Canvases").transform.GetChild(3).GetComponent<Canvas>().gameObject.SetActive(true);
            canvas = GameObject.Find("Canvases/Canvas4").GetComponent<Canvas>();
            canvas.worldCamera = this.GetComponent<Camera>();
        }
        else if (photonView.Owner.NickName == "5")
        {
            GameObject.Find("Canvases").transform.GetChild(4).GetComponent<Canvas>().gameObject.SetActive(true);
            canvas = GameObject.Find("Canvases/Canvas5").GetComponent<Canvas>();
            canvas.worldCamera = this.GetComponent<Camera>();
        }
        else if (photonView.Owner.NickName == "6")
        {
            GameObject.Find("Canvases").transform.GetChild(5).GetComponent<Canvas>().gameObject.SetActive(true);
            canvas = GameObject.Find("Canvases/Canvas6").GetComponent<Canvas>();
            canvas.worldCamera = this.GetComponent<Camera>();
        }
        else if (photonView.Owner.NickName == "7")
        {
            GameObject.Find("Canvases").transform.GetChild(6).GetComponent<Canvas>().gameObject.SetActive(true);
            canvas = GameObject.Find("Canvases/Canvas7").GetComponent<Canvas>();
            //canvas.worldCamera = this.GetComponent<Camera>();
        }
        else if (photonView.Owner.NickName == "8")
        {
            GameObject.Find("Canvases").transform.GetChild(7).GetComponent<Canvas>().gameObject.SetActive(true);
            canvas = GameObject.Find("Canvases/Canvas8").GetComponent<Canvas>();
            canvas.worldCamera = this.GetComponent<Camera>();
        }
        else if (photonView.Owner.NickName == "9")
        {
            GameObject.Find("Canvases").transform.GetChild(8).GetComponent<Canvas>().gameObject.SetActive(true);
            canvas = GameObject.Find("Canvases/DocentCanvas").GetComponent<Canvas>();
            canvas.worldCamera = this.GetComponent<Camera>();
        }
    }

    public void setCanvasInactive()
    {
        canvas.gameObject.SetActive(false);
    }

    /**
     * Maps the ViewFinderCamera to the tracker position
     */
    void MapTrackerPosition(GameObject tracker)
    {
        try
        {
            
            Vector3 newPosition = tracker.transform.position;
            Quaternion newRotation = tracker.transform.rotation;

            newPosition.y += .1f;
            newRotation *= cameraOffset;

            transform.position = newPosition;
            transform.rotation = newRotation;
            Debug.Log(transform.position);
            //transform.position = tracker.transform.position;
            //transform.rotation = tracker.transform.rotation;
        }
        
        catch
        {
            Debug.Log("Failed mapping of tracker: " + photonView.Owner.NickName + " , Attempting to connect again");
            trackerSetup();
        }
    }

    /**
     * Going to be used if necessary for when tablets change scenes
     */
    public void reset()
    {
        //Start();
    }

    /**
     * Immediately finds trackers
     */
    public void trackerSetup()
    {
        Tracker1 = GameObject.Find("Tracker1(Clone)");
        Tracker2 = GameObject.Find("Tracker2(Clone)");
        Tracker3 = GameObject.Find("Tracker3(Clone)");
        Tracker4 = GameObject.Find("Tracker4(Clone)");
        Tracker5 = GameObject.Find("Tracker5(Clone)");
        Tracker6 = GameObject.Find("Tracker6(Clone)");
        Tracker7 = GameObject.Find("Tracker7(Clone)");
        Tracker8 = GameObject.Find("Tracker8(Clone)");
        Tracker9 = GameObject.Find("Tracker9(Clone)");
    }

    /**
     * Finds trackers after a few seconds
     */
    IEnumerator FindTrackerAfterFewSeconds()
    {
        yield return new WaitForSeconds(3f);
        Tracker1 = GameObject.Find("Tracker1(Clone)");
        Tracker2 = GameObject.Find("Tracker2(Clone)");
        Tracker3 = GameObject.Find("Tracker3(Clone)");
        Tracker4 = GameObject.Find("Tracker4(Clone)");
        Tracker5 = GameObject.Find("Tracker5(Clone)");
        Tracker6 = GameObject.Find("Tracker6(Clone)");
        Tracker7 = GameObject.Find("Tracker7(Clone)");
        Tracker8 = GameObject.Find("Tracker8(Clone)");
        Tracker9 = GameObject.Find("Tracker9(Clone)");
    }

    // Daniel's code
    public void CallRPCChangeCamera(int CameraNumber)
    {
        photonView.RPC("ChangeCamera", RpcTarget.MasterClient, CameraNumber);
    }

    // Daniel's code
    public void CallRPCResetCamera()
    {
        photonView.RPC("ResetCamera", RpcTarget.MasterClient);
    }

    public void CallRPCChangeScene(int scene)
    {
        photonView.RPC("ChangeScene", RpcTarget.MasterClient);
    }

    public void CallRPCOpenHamburger()
    {
        photonView.RPC("OpenHamburger", RpcTarget.MasterClient);
    }

    // Daniel's code
    [PunRPC]
    public void ChangeCamera(int CameraNumber)
    {
        DocentManager.inst.ChangeCamera(CameraNumber);
    }

    // Daniel's code
    [PunRPC]
    public void ResetCamera()
    {
        DocentManager.inst.ResetCamera();
    }

    [PunRPC]
    public void ChangeScene(int scene)
    {
        DocentManager.inst.ChangeScene(scene);
    }

    [PunRPC]
    public void OpenHamburger()
    {
        DocentManager.inst.OpenHamburger();
    }
}

