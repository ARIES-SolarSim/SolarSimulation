using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
public class CameraSetup : MonoBehaviour
{
    public GameObject Tracker1, Tracker2, Tracker3, Tracker4, Tracker5, Tracker6, Tracker7, Tracker8, Tracker9;
    private Canvas canvas;
    private ViewTypeObserver view;
    private PhotonView photonView;
    private PhotonTransformView photonTransformView;
    [SerializeField]
    private GameObject[] viewFinderCameras;
    private Camera camera;
    private Camera myCamera;

    public Quaternion cameraOffset;
    void Start()
    {
        if (photonView == null)
        {
            photonView = GetComponent<PhotonView>();
            photonTransformView = GetComponent<PhotonTransformView>();
        }
        view = GameObject.FindGameObjectWithTag("view").GetComponent<ViewTypeObserver>();
        myCamera = FindObjectOfType<Camera>();
        StartCoroutine(FindTrackerAfterFewSeconds()); //give few seconds for the systems to settle
        this.gameObject.name = photonView.Owner.NickName;
        trackerSetup();
        //setCanvas();

        if (photonView.IsMine) //revmoe the tag so that myself is not disabled in the update funciton
        {
            Debug.Log("setting tag myself for " + photonView.Owner.NickName);
            this.gameObject.tag = "Myself";
        }
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

            viewFinderCameras[i].SetActive(false);

        }

    }

    /**
     * Sets the canvases of each tablet so it has its own UI
     */
  /*  void setCanvas()
    {
        if (photonView.Owner.NickName == "1")
        {
            GameObject.Find("UI_Canvases").transform.GetChild(0).GetComponent<Canvas>().gameObject.SetActive(true);
            canvas = GameObject.Find("UI_Canvases/Canvas1").GetComponent<Canvas>();
            canvas.worldCamera = this.GetComponent<Camera>();
        }
        else if (photonView.Owner.NickName == "2")
        {
            GameObject.Find("UI_Canvases").transform.GetChild(1).GetComponent<Canvas>().gameObject.SetActive(true);
            canvas = GameObject.Find("UI_Canvases/Canvas2").GetComponent<Canvas>();
            canvas.worldCamera = this.GetComponent<Camera>();
        }
        else if (photonView.Owner.NickName == "3")
        {
            GameObject.Find("UI_Canvases").transform.GetChild(2).GetComponent<Canvas>().gameObject.SetActive(true);
            canvas = GameObject.Find("UI_Canvases/Canvas3").GetComponent<Canvas>();
            canvas.worldCamera = this.GetComponent<Camera>();
        }
        else if (photonView.Owner.NickName == "4")
        {

            Debug.Log("here");
            GameObject.Find("UI_Canvases").transform.GetChild(3).GetComponent<Canvas>().gameObject.SetActive(true);
            canvas = GameObject.Find("UI_Canvases/Canvas4").GetComponent<Canvas>();
            canvas.worldCamera = this.GetComponent<Camera>();
        }
        else if (photonView.Owner.NickName == "5")
        {
            GameObject.Find("UI_Canvases").transform.GetChild(4).GetComponent<Canvas>().gameObject.SetActive(true);
            canvas = GameObject.Find("UI_Canvases/Canvas5").GetComponent<Canvas>();
            canvas.worldCamera = this.GetComponent<Camera>();
        }
        else if (photonView.Owner.NickName == "6")
        {
            GameObject.Find("UI_Canvases").transform.GetChild(5).GetComponent<Canvas>().gameObject.SetActive(true);
            canvas = GameObject.Find("UI_Canvases/Canvas6").GetComponent<Canvas>();
            canvas.worldCamera = this.GetComponent<Camera>();
        }
        else if (photonView.Owner.NickName == "7")
        {
            GameObject.Find("UI_Canvases").transform.GetChild(6).GetComponent<Canvas>().gameObject.SetActive(true);
            canvas = GameObject.Find("UI_Canvases/Canvas7").GetComponent<Canvas>();
            canvas.worldCamera = this.GetComponent<Camera>();
        }
        else if (photonView.Owner.NickName == "8")
        {
            GameObject.Find("UI_Canvases").transform.GetChild(7).GetComponent<Canvas>().gameObject.SetActive(true);
            canvas = GameObject.Find("UI_Canvases/Canvas8").GetComponent<Canvas>();
            canvas.worldCamera = this.GetComponent<Camera>();
        }
        else if (photonView.Owner.NickName == "9")
        {
            GameObject.Find("UI_Canvases").transform.GetChild(8).GetComponent<Canvas>().gameObject.SetActive(true);
            canvas = GameObject.Find("UI_Canvases/DocentCanvas").GetComponent<Canvas>();
            canvas.worldCamera = this.GetComponent<Camera>();
        }
    }*/

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
            Debug.Log("VIEW: " + view.view);
            if (view.view != 4)
            {
                Vector3 newPosition = tracker.transform.position;
                Quaternion newRotation = tracker.transform.rotation;

                newPosition.y += .1f;
                newRotation *= cameraOffset;

                transform.position = newPosition;
                transform.rotation = newRotation;
                //transform.position = tracker.transform.position;
                //transform.rotation = tracker.transform.rotation;
            }
            else
            {
                Debug.Log("WE ARE IN OCEAN VIEW");
                if (null == camera)
                {
                    camera = GameObject.FindGameObjectWithTag("SceneCamera").GetComponent<Camera>();
                }
                transform.position = camera.transform.position;
                transform.rotation = camera.transform.rotation;
                myCamera.orthographic = true;
                myCamera.orthographicSize = 8;



            }

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

    

    
}

