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
    private int targetViewType; //The view type that should be traveled to
    private int steps = -1; //Used for scene transitons
    public int otherScene; //place holder to represent the other scene to travel to (Once more than 2 scenes exist, this needs to be redone)
    private GameObject Tracker1, Tracker2, Tracker3, Tracker4, Tracker5, Tracker6, Tracker7, Tracker8, Tracker9;
    public RotateScript tempMoonRotate;
    public MeshScaler tempMoonScale;

    private bool transistion = false;

    public static TrailRenderer[] trails;

    private void Start()
    {
        currentViewType = 1;
        targetViewType = 1;
        PhotonNetwork.AutomaticallySyncScene = true;
        if (otherScene == 2)
        {
            trails = new TrailRenderer[FindObjectsOfType<PlanetController>().Length - 2];
        }
    }

    void Update()
    {
        if(transform.localPosition.y == 1)
        {
            if (otherScene == 2)
            {
                //moon.gameObject.SetActive(true);
            }
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
            transform.localPosition = new Vector3(0, 0, 1);

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
                transform.localPosition = new Vector3(0, 0, 0);
                steps = 0; //Finished view type transistion
                currentViewType = targetViewType;
            }
        }
    }
    /*
    void bringTrailsBack()
    {
        for (int i = 0; i < trails.Length; i++)
        {
            // turn on trails
            trails[i].enabled = true;
        }
        //mercuryTrail.SetActive(true);
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
        //mercuryTrail.SetActive(false);
    }*/

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
    } //Macey What does this do? Can't we remove it or make this more efficient
}
