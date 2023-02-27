using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
public class CameraSetup : MonoBehaviour
{
    private GameObject Tracker1, Tracker2, Tracker3, Tracker4, Tracker5, Tracker6, Tracker7, Tracker8, Tracker9;
    private Canvas canvas;
    private TextMeshProUGUI deviceNumberText;

    private PhotonView photonView;
    [SerializeField]
    private GameObject[] viewFinderCameras;

    public Quaternion cameraOffset;
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        StartCoroutine(FindTrackerAfterFewSeconds()); //give few seconds for the systems to settle
        this.gameObject.name = photonView.Owner.NickName;

        //trackerSetup();
        setCanvas();

        if (photonView.IsMine) //revmoe the tag so that myself is not disabled in the update funciton
        {
            this.gameObject.tag = "Untagged";
        }

        //deviceNumberText.text = "#" + photonView.Owner.NickName; //print the device number on the screen
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.Owner.NickName == "1")
        {
            MapTrackerPosition(Tracker1);
            //this.GetComponent<Camera>().cullingMask &= ~(1 << LayerMask.NameToLayer("DocentUI"));
        }
        else if (photonView.Owner.NickName == "2")
        {
            MapTrackerPosition(Tracker2);
            //this.GetComponent<Camera>().cullingMask &= ~(1 << LayerMask.NameToLayer("DocentUI"));
        }
        else if (photonView.Owner.NickName == "3")
        {
            MapTrackerPosition(Tracker3);
            //this.GetComponent<Camera>().cullingMask &= ~(1 << LayerMask.NameToLayer("DocentUI"));
        }
        else if (photonView.Owner.NickName == "4")
        {
            MapTrackerPosition(Tracker4);
            //this.GetComponent<Camera>().cullingMask &= ~(1 << LayerMask.NameToLayer("DocentUI"));
        }
        else if (photonView.Owner.NickName == "5")
        {
            MapTrackerPosition(Tracker5);
            //this.GetComponent<Camera>().cullingMask &= ~(1 << LayerMask.NameToLayer("DocentUI"));
        }
        else if (photonView.Owner.NickName == "6")
        {
            MapTrackerPosition(Tracker6);
            //this.GetComponent<Camera>().cullingMask &= ~(1 << LayerMask.NameToLayer("DocentUI"));
        }
        else if (photonView.Owner.NickName == "7")
        {
            MapTrackerPosition(Tracker7);
            //this.GetComponent<Camera>().cullingMask &= ~(1 << LayerMask.NameToLayer("DocentUI"));
        }
        else if (photonView.Owner.NickName == "8")
        {
            MapTrackerPosition(Tracker8);
            //this.GetComponent<Camera>().cullingMask &= ~(1 << LayerMask.NameToLayer("DocentUI"));
        }
        else if (photonView.Owner.NickName == "9")
        {
            MapTrackerPosition(Tracker9);
            //this.GetComponent<Camera>().cullingMask &= ~(1 << LayerMask.NameToLayer("NormalUI"));
        }


        //disable the camera that is not my view
        viewFinderCameras = GameObject.FindGameObjectsWithTag("ViewFinderCamera");
        for (int i = 0; i < viewFinderCameras.Length; i++)
        {
            viewFinderCameras[i].SetActive(false);
        }

    }

    void trackerSetup()
    {
        Tracker1 = PhotonNetwork.Instantiate("Tracker1", transform.position, transform.rotation);
        Tracker2 = PhotonNetwork.Instantiate("Tracker2", transform.position, transform.rotation);
        Tracker3 = PhotonNetwork.Instantiate("Tracker3", transform.position, transform.rotation);
        Tracker4 = PhotonNetwork.Instantiate("Tracker4", transform.position, transform.rotation);
        Tracker5 = PhotonNetwork.Instantiate("Tracker5", transform.position, transform.rotation);
        Tracker6 = PhotonNetwork.Instantiate("Tracker6", transform.position, transform.rotation);
        Tracker7 = PhotonNetwork.Instantiate("Tracker7", transform.position, transform.rotation);
        Tracker8 = PhotonNetwork.Instantiate("Tracker8", transform.position, transform.rotation);
        Tracker9 = PhotonNetwork.Instantiate("Tracker9", transform.position, transform.rotation);
    }

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
            canvas.worldCamera = this.GetComponent<Camera>();
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
            //transform.position = tracker.transform.position;
            //transform.rotation = tracker.transform.rotation;
        }
        catch
        {
            Debug.Log("Loading a player...");
        }
    }

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

