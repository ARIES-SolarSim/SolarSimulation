using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

/*
 * This class is responsible for taking in information from the VR user, and telling all the builds to do the same action at the same time.
 */
public class ViewTypeObserver : MonoBehaviour
{
    public int currentViewType; //The current viewtype that the scene is displaying
    public int targetViewType;
    private int steps = -1;
    public int otherScene; //place holder
    private GameObject Tracker1, Tracker2, Tracker3, Tracker4, Tracker5, Tracker6, Tracker7, Tracker8, Tracker9;
    private GameObject mercuryTrail;
    //public PlanetController moon;

    //public GameObject earth;

    public RotateScript tempMoonRotate;
    public MeshScaler tempMoonScale;

    private bool transistion = false;

    public static TrailRenderer[] trails;

    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        if (otherScene == 2)
        {
            trails = new TrailRenderer[FindObjectsOfType<PlanetController>().Length - 2];
        }
        mercuryTrail = GameObject.Find("Trail");

        // ONLY UNCOMMENT IF DEBUGGING WITH UNITY REMOTE
        //trackerSetup();
    }

    void Update()
    {
        if(transform.localPosition.y != 0)
        {
            if (otherScene == 2)
            {
                //moon.gameObject.SetActive(true);
            }
            if (transform.localPosition.y == 1)
            {
                PhotonNetwork.LoadLevel(1);
            }
            else
            {
                PhotonNetwork.LoadLevel((int)transform.localPosition.y - 1);
            }
            transform.localPosition = Vector3.zero;
            updateCameras();
        }
        if(transform.localPosition.x == 1 && otherScene == 2) //View Type
        {
            targetViewType = (currentViewType == 1 ? 2 : 1);
            transform.localPosition = Vector3.zero;
            steps = 0;
            FindObjectOfType<RotateScript>().view = targetViewType;
            FindObjectOfType<RotateScript>().changing = true;
        }
        if(currentViewType != targetViewType)
        {
            transform.localPosition = new Vector3(0, 0, 3);

            steps++;
            UniverseController.orbiting = false;
            FindObjectOfType<UniverseController>().gameObject.transform.localEulerAngles = Vector3.zero; //May need to become smooth
            foreach (PlanetIdentifier pi in FindObjectsOfType<PlanetIdentifier>())
            {
                if(targetViewType == 1)
                {
                    pi.showArrow();
                }
                else
                {
                    pi.hideArrow();
                }
            }
            foreach (PlanetController pc in FindObjectsOfType<PlanetController>())
            {
                pc.changeViewType(targetViewType);
            }
            if (UniverseController.changeState == 1)
            {
                transistion = true;
            }
            if (steps == UniverseController.changeDuration && transistion)
            {
                Debug.Log("Transistion Complete, returning steps to -1");
                steps = 0; //Finished view type transistion
                currentViewType = targetViewType;
            }
        }

        if (otherScene == 2)
        {
            if (UniverseController.orbiting && !mercuryTrail.activeSelf)
            {
                bringTrailsBack();
            }

            else if (!UniverseController.orbiting && mercuryTrail.activeSelf)
            {
                hideTrailsTemp();
            }
        }
    }

    public void changeScene(int i)
    {
        if ((i == 2 && currentViewType == 1) || (i == 1 && currentViewType == 2))
        {
            Debug.Log("going to view 2");
            transform.position = new Vector3(1, 0, 0);
        }

        else if (i == 2)
        {
            PhotonNetwork.LoadLevel(1);
            transform.position = new Vector3(1, 0, 0);
        }

        else if (currentViewType != i)
        {
            transform.position = new Vector3(0, i, 0);
        }
    }

    void bringTrailsBack()
    {
        for (int i = 0; i < trails.Length; i++)
        {
            // turn on trails
            trails[i].enabled = true;
        }

        mercuryTrail.SetActive(true);
    }

    void hideTrailsTemp()
    {
        int i = 0;

        foreach (PlanetController pc in FindObjectsOfType<PlanetController>())
        {
            if (pc.ID == 0 || pc.ID == 1)
            {
                continue;
            }
            // turn off trails
            trails[i] = pc.gameObject.GetComponent<TrailRenderer>();
            trails[i].Clear();
            trails[i].enabled = false;
            i++;
        }
        mercuryTrail.SetActive(false);
    }

    void updateCameras()
    {
        Object[] all = FindObjectsOfType(this.gameObject.GetType(), true);

        for (int i = 0; i < all.Length; i++)
        {
            if (all[i].name == "1")
            {
                Tracker1 = all[i] as GameObject;
            }

            else if (all[i].name == "2")
            {
                Tracker2 = all[i] as GameObject;
            }

            else if (all[i].name == "3")
            {
                Tracker3 = all[i] as GameObject;
            }

            else if (all[i].name == "4")
            {
                Tracker4 = all[i] as GameObject;
            }

            else if (all[i].name == "5")
            {
                Tracker5 = all[i] as GameObject;
            }

            else if (all[i].name == "6")
            {
                Tracker6 = all[i] as GameObject;
            }

            else if (all[i].name == "7")
            {
                Tracker7 = all[i] as GameObject;
            }

            else if (all[i].name == "8")
            {
                Tracker8 = all[i] as GameObject;
            }

            else if (all[i].name == "9")
            {
                Tracker9 = all[i] as GameObject;
            }

        }

        if (Tracker1 != null)
        {
            Tracker1.SetActive(true);
            Tracker1.GetComponent<CameraSetup>().reset();
        }

        if (Tracker2 != null)
        {
            Tracker2.SetActive(true);
            Tracker2.GetComponent<CameraSetup>().reset();
        }

        if (Tracker3 != null)
        {
            Tracker3.SetActive(true);
            Tracker3.GetComponent<CameraSetup>().reset();
        }

        if (Tracker4 != null)
        {
            Tracker4.SetActive(true);
            Tracker4.GetComponent<CameraSetup>().reset();
        }

        if (Tracker5 != null)
        {
            Tracker5.SetActive(true);
            Tracker5.GetComponent<CameraSetup>().reset();
        }

        if (Tracker6 != null)
        {
            Tracker6.SetActive(true);
            Tracker6.GetComponent<CameraSetup>().reset();
        }

        if (Tracker7 != null)
        {
            Tracker7.SetActive(true);
            Tracker7.GetComponent<CameraSetup>().reset();
        }

        if (Tracker8 != null)
        {
            Tracker8.SetActive(true);
            Tracker8.GetComponent<CameraSetup>().reset();
        }

        if (Tracker9 != null)
        {
            Tracker9.SetActive(true);
            Tracker9.GetComponent<CameraSetup>().reset();
        }
    }

    void trackerSetup()
    {
        PhotonNetwork.Instantiate("Tracker1", transform.position, transform.rotation);
        PhotonNetwork.Instantiate("Tracker2", transform.position, transform.rotation);
        PhotonNetwork.Instantiate("Tracker3", transform.position, transform.rotation);
        PhotonNetwork.Instantiate("Tracker4", transform.position, transform.rotation);
        PhotonNetwork.Instantiate("Tracker5", transform.position, transform.rotation);
        PhotonNetwork.Instantiate("Tracker6", transform.position, transform.rotation);
        PhotonNetwork.Instantiate("Tracker7", transform.position, transform.rotation);
        PhotonNetwork.Instantiate("Tracker8", transform.position, transform.rotation);
        PhotonNetwork.Instantiate("Tracker9", transform.position, transform.rotation);
    }
}
