using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class is responsible for taking in information from the VR user, and telling all the builds to do the same action at the same time.
 */
public class ViewTypeObserver : MonoBehaviour
{
    public int currentViewType; //The current viewtype that the scene is displaying
    public int targetViewType;
    public int changeCode;
    public PlanetController earth;
    public PlanetController sun;
    public Light atmosphereLight;
    public int steps = -1;
    private int arrowTime = 200;

    public RotateScript tempMoonRotate;
    public MeshScaler tempMoonScale;

    public float viewThreeY = -34.29f; //May need to double check

    private bool transistion = false;

    private void Start()
    {
        currentViewType = 1;
    }

    void Update()
    {
        if(transform.localPosition.x == 1)
        {
            targetViewType = (currentViewType == 1 ? 2 : 1);
        }
        if(currentViewType != targetViewType)
        {
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
            steps = 0;
        }
        /*
        if(transform.localPosition.x != currentViewType)
        {
            previousViewType = currentViewType;
            currentViewType = (int)transform.localPosition.x;

            tempMoonRotate.view = currentViewType;
            tempMoonScale.view = currentViewType;

            switch (currentViewType) //Sets up unique actions that need to occur depending on the previous and current viewtype
            {
                case 1: //starting, will not run on boot but will any time view is changed
                    if (previousViewType == 3)
                    {
                        changeCode = 1;
                    }
                    else //2
                    {
                        changeCode = 4;
                    }
                    break;
                case 2:
                    if (previousViewType == 3)
                    {
                        changeCode = 2;
                    }
                    else //1
                    {
                        changeCode = 5;
                    }
                    break;
                case 3:
                    if (previousViewType == 2)
                    {
                        changeCode = 3;
                    }
                    else //1
                    {
                        changeCode = 0;
                    }
                    break;
            }
            UniverseController.orbiting = false; //Stops the planets from orbiting and tells UniverseController to change the planets data for the given scene.
            foreach (PlanetController pc in FindObjectsOfType<PlanetController>())
            {
                pc.changeViewType(currentViewType);
            }
            steps = 0;
        }
        if(UniverseController.changeState == 1)
        {
            transistion = true;
        }
        if (steps >= 0)//Starts to complete actions after planets have slowed down
        {
            if(steps == 0 && transistion)
            {
                Debug.Log("Entered Branch with change code of " + changeCode);
                FindObjectOfType<UniverseController>().gameObject.transform.localEulerAngles = Vector3.zero;
            }
            //Regardless, rotate CU back to 0,0,0s
            //TODO

            //Debug.Log("Steps: " + steps + " Bool1: " + (changeCode == 0 || changeCode == 5) + " Bool2: " + (steps == UniverseController.changeDuration - 1));
            if ((changeCode == 1 || changeCode == 4) && steps == UniverseController.changeDuration - arrowTime && transistion)//Enable Arrows
            {
                Debug.Log("Showing Arrows");
                foreach (PlanetIdentifier pi in FindObjectsOfType<PlanetIdentifier>())
                {
                    pi.showArrow();
                }
            }
            if ((changeCode == 0 || changeCode == 5) && steps == arrowTime && transistion) //Disable Arrows
            {
                Debug.Log("Hiding Arrows");
                foreach (PlanetIdentifier pi in FindObjectsOfType<PlanetIdentifier>())
                {
                    pi.hideArrow();
                }
            }
            if (changeCode == 0 || changeCode == 3) //To View3
            {
                if (UniverseController.changeState == 0) // Decel
                {
                    float newX = UniverseController.sigmoidRounded(UniverseController.changeSteps * 500f/150f) * (-1 * earth.transform.localPosition).x;
                    float newY = 1f;//UniverseController.sigmoidRounded(UniverseController.changeSteps) * ((-1 * earth.transform.localPosition).y - 1) + 1;
                    float newZ = UniverseController.sigmoidRounded(UniverseController.changeSteps * 500f / 150f) * (-1 * earth.transform.localPosition).z;
                    FindObjectOfType<UniverseController>().gameObject.transform.position = new Vector3(newX, newY, newZ); //Moves COU 

                    foreach(PlanetController pc in FindObjectsOfType<PlanetController>())
                    {
                        if(pc.ID != 0 && pc.ID != 3)
                        {
                            pc.transform.localScale = (-1 * UniverseController.sigmoidRounded(UniverseController.changeSteps * 500f / 150f) + 1) * Vector3.one;
                        }
                    }
                    Debug.Log("Moving - X: " + newX + " Y: " + newY + " Z: " + newZ);
                    if (UniverseController.changeSteps == UniverseController.accDuration)
                    {
                        FindObjectOfType<UniverseController>().cameraLockedPlanet = earth;
                        FindObjectOfType<UniverseController>().gameObject.transform.position = new Vector3(0f, 1f, 0f);
                        Debug.Log("Done - X: " + 0 + " Y: " + 1 + " Z: " + 0);
                    }
                }
                if (UniverseController.changeState == 1)
                {
                    FindObjectOfType<UniverseController>().gameObject.transform.position = new Vector3(0f, -1 * earth.diameter / 2 - 1, 0f);
                    Debug.Log("MovingP2 - X: " + 0 + " Y: " + earth.diameter / 2 + " Z: " + 0);
                    if (steps == UniverseController.changeDuration - 1)
                    {
                        Debug.Log("Final");
                        FindObjectOfType<UniverseController>().gameObject.transform.position = new Vector3(0f, viewThreeY, 0f);
                    }
                }
            }
            if (changeCode == 1 || changeCode == 2) //From View3
            {
                FindObjectOfType<UniverseController>().gameObject.transform.position = new Vector3(0, 1, 0); //Make smooth

                foreach (PlanetController pc in FindObjectsOfType<PlanetController>())
                {
                    if (pc.ID != 0 && pc.ID != 3)
                    {
                        pc.transform.localScale = (UniverseController.sigmoidRounded(UniverseController.changeSteps * 500f / 150f)) * Vector3.one;
                    }
                }

                FindObjectOfType<UniverseController>().cameraLockedPlanet = sun;
                /*
                if (UniverseController.changeState == 0) // Decel
                {
                    float newX = UniverseController.sigmoidRounded(UniverseController.changeSteps * 500f / 150f) * (-1 * sun.transform.localPosition).x;
                    float newY = viewThreeY;//UniverseController.sigmoidRounded(UniverseController.changeSteps) * ((-1 * earth.transform.localPosition).y - 1) + 1;
                    float newZ = UniverseController.sigmoidRounded(UniverseController.changeSteps * 500f / 150f) * (-1 * sun.transform.localPosition).z;
                    FindObjectOfType<UniverseController>().gameObject.transform.position = new Vector3(newX, newY, newZ);
                    foreach (PlanetController pc in FindObjectsOfType<PlanetController>())
                    {
                        if (pc.ID != 0 && pc.ID != 3)
                        {
                            pc.transform.localScale = (UniverseController.sigmoidRounded(UniverseController.changeSteps * 500f / 150f)) * Vector3.one;
                        }
                    }
                    Debug.Log("Moving - X: " + newX + " Y: " + newY + " Z: " + newZ);
                    if (UniverseController.changeSteps == UniverseController.accDuration)
                    {
                        FindObjectOfType<UniverseController>().cameraLockedPlanet = sun;
                        FindObjectOfType<UniverseController>().gameObject.transform.position = new Vector3(0f, viewThreeY, 0f);
                        Debug.Log("Done - X: " + 0 + " Y: " + viewThreeY + " Z: " + 0);
                    }
                }
                if (UniverseController.changeState == 1)
                {
                    FindObjectOfType<UniverseController>().gameObject.transform.position = new Vector3(0f, -1 * sun.diameter / 2 - 1, 0f);
                    Debug.Log("MovingP2 - X: " + 0 + " Y: " + sun.diameter / 2 + " Z: " + 0);
                    if (steps == UniverseController.changeDuration - 1)
                    {
                        Debug.Log("Final");
                        FindObjectOfType<UniverseController>().gameObject.transform.position = new Vector3(0f, 1f, 0f);
                    }
                }*/
            }
            /*
            if (changeCode == 1 || changeCode == 2) //From View3
            {
                if(steps == 0)
                {
                    Debug.Log("Moving to 0, 1, 0");
                }
                float slope = UniverseController.sigmoidRounded(steps) * (1 - viewThreeY) + viewThreeY; //Should smooth out that transition
                FindObjectOfType<UniverseController>().gameObject.transform.position = new Vector3(0, slope, 0);
            }
            if (changeCode == 0 || changeCode == 3) //To View3
            {
                if (steps == 0)
                {
                    Debug.Log("Moving to 0, " + viewThreeY + ", 0");
                }
                //Vector3 d = -1 * UniverseController.sigmoid(steps) * (earth.transform.localPosition - Vector3.zero);
                float xTarget = (steps == UniverseController.changeDuration - 1) ? 0 : UniverseController.sigmoid(steps) * (0 - earth.transform.localPosition.x);
                float yTarget = UniverseController.sigmoid(steps) * viewThreeY;
                float zTarget = (steps == UniverseController.changeDuration - 1) ? 0 : UniverseController.sigmoid(steps) * (0 - earth.transform.localPosition.z);
                FindObjectOfType<UniverseController>().gameObject.transform.position = new Vector3(xTarget, yTarget, zTarget);
                if (steps == UniverseController.changeDuration - 1)
                {
                    FindObjectOfType<UniverseController>().cameraLockedPlanet = earth;
                }
            }*/
            /*
            steps++;
            if(steps == UniverseController.changeDuration && transistion)
            {
                Debug.Log("Transistion Complete, returning steps to -1");
                steps = -1; //Finished view type transistion
                transistion = false;
            }
        }
    }*/
}
