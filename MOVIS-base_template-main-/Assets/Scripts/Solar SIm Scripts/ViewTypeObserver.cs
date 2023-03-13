using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

/*
 * This class is responsible for taking in information from the VR user, and telling all the builds to do the same action at the same time.
 */
public class ViewTypeObserver : MonoBehaviour
{
    private int currentViewType; //The current viewtype that the scene is displaying
    private int targetViewType;
    private int steps = -1;
    public int otherScene; //place holder
    private GameObject Tracker1, Tracker2, Tracker3, Tracker4, Tracker5, Tracker6, Tracker7, Tracker8, Tracker9;

    //public GameObject earth;

    public RotateScript tempMoonRotate;
    public MeshScaler tempMoonScale;

    private bool transistion = false;

    private void Start()
    {
        currentViewType = 1;
        targetViewType = 1;
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Update()
    {
        if(transform.localPosition.y == 1)
        {
            PhotonNetwork.LoadLevel(otherScene);
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
            Tracker2.GetComponent<CameraSetup>().reset();
        }

        if (Tracker3 != null)
        {
            Tracker3.GetComponent<CameraSetup>().reset();
        }

        if (Tracker4 != null)
        {
            Tracker4.GetComponent<CameraSetup>().reset();
        }

        if (Tracker5 != null)
        {
            Tracker5.GetComponent<CameraSetup>().reset();
        }

        if (Tracker6 != null)
        {
            Tracker6.GetComponent<CameraSetup>().reset();
        }

        if (Tracker7 != null)
        {
            Tracker7.GetComponent<CameraSetup>().reset();
        }

        if (Tracker8 != null)
        {
            Tracker8.GetComponent<CameraSetup>().reset();
        }

        if (Tracker9 != null)
        {
            Tracker9.GetComponent<CameraSetup>().reset();
        }
    }
}
