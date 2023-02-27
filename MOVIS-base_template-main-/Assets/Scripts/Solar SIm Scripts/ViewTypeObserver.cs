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
    public int steps = -1;
    public int otherScene; //place holder

    public GameObject earth;

    public RotateScript tempMoonRotate;
    public MeshScaler tempMoonScale;

    public float viewThreeY = -34.29f; //May need to double check

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
}
