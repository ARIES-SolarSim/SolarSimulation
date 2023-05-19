using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * This class is responsible for taking in information from the VR user, and telling all the builds to do the same action at the same time.
 */
public class ViewTypeObserver : MonoBehaviour
{

    public GameObject loaderCanvas;
    public Image progressBar;
    public Image astronaut;

    private int currentViewType; //The current viewtype that the scene is displaying
    public static int targetViewType; //The view type that should be traveled to
    private int steps = -1; //Used for scene transitons
    public int otherScene; //place holder to represent the other scene to travel to

    public RotateScript tempMoonRotate; // moon rotation script
    public MeshScaler tempMoonScale; // moon mesh script

    private bool transistion = false;


    

    public static TrailRenderer[] trails;

    private static string[] levelNames = { "Room1", "Room1", "Room2", "", "", "" }; // names of each scene, in order (update as we add more)

    private void Start()
    {
        currentViewType = System.Array.IndexOf(levelNames, SceneManager.GetActiveScene().name) + 1;
        targetViewType = currentViewType;
        //Debug.Log("Started at " + currentViewType);
        PhotonNetwork.AutomaticallySyncScene = true;
        if (otherScene == 2)
        {
            trails = new TrailRenderer[FindObjectsOfType<PlanetController>().Length - 2];
        }
    }

    void Update()
    {
        int y = (int)transform.localPosition.y;

        // Switching to different views if the view we try to switch to is not the one we're in already
        if (y > 0 && y <= 6 && y != currentViewType)
        {
            

            // Scene 1 has special cases
            if (y == 1)
            {
                // If not in room 1, go to it
                if (currentViewType > 2)
                {
                    PhotonNetwork.LoadLevel(levelNames[0]);
                    currentViewType = 1;
                    transform.localPosition = Vector3.zero;
                }

                // If in room 1, toggle viewtype
                else
                {
                    transform.localPosition = new Vector3(1, 0, 0);
                }
            }

            // Scene 2 has special cases
            else if (y == 2)
            {
                // If not in room 1, go to it, then immediately toggle to view 2
                // (does not work because new scene reloads everything. May need a separate
                // scene for going directly into view 2, but don't worry about that right now)
                if (currentViewType > 2)
                {
                    PhotonNetwork.LoadLevel(levelNames[1]);
                    currentViewType = 1;
                    transform.localPosition = Vector3.zero;
                }

                // If in room 1, toggle
                else
                {
                    currentViewType = 1;
                    transform.localPosition = new Vector3(1, 0, 0);
                }
            }

            // Index of all other scenes is (scene number - 1)
            else
            {
                Debug.Log("Loading scene: " + levelNames[y - 1]);
                //PhotonNetwork.LoadLevel(levelNames[y - 1]);
                StartCoroutine(LoadingSomeLevel("Room2"));
                currentViewType = y;
                targetViewType = y; // Set this to avoid transition case
                transform.localPosition = Vector3.zero;
            }
            //Debug.Log("View currently: " + currentViewType + ", target: " + targetViewType);
        }

        // If trying to switch to the scene we're already in
        if (y == currentViewType)
        {
            transform.localPosition = Vector3.zero;
        }

        // Going from view 1 to view 2 or vice versa
        if (transform.localPosition.x == 1 && otherScene == 2)
        {
            //Debug.Log("x = 1, otherscene = 2");
            targetViewType = (currentViewType == 1 ? 2 : 1);
            transform.localPosition = Vector3.zero;
            steps = 0;
            FindObjectOfType<RotateScript>().view = targetViewType;
            FindObjectOfType<RotateScript>().changing = true;
        }

        // Transitioning between view 1 and view 2 or vice versa
        if (currentViewType != targetViewType)
        {
            //Debug.Log("Mismatch");
            steps++;
            UniverseController.orbiting = false;
            FindObjectOfType<UniverseController>().gameObject.transform.localEulerAngles = Vector3.zero; //May need to become smooth

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

    /**
    * Called by pressing the scene buttons on the docent UI tablet
    */
    public void changeScene(int i)
    {
        transform.localPosition = new Vector3(0, i, 0);
    }

    IEnumerator LoadingSomeLevel(string sceneValue)
    {
        loaderCanvas.SetActive(true);

        
     

        float progress = 0f;

        while (progress < 1f)
        {
            
            progress += 0.5f *0.07f;
            astronaut.rectTransform.localPosition = new  Vector3(-157 +(progress * 300), 25.5f, 0);
            progressBar.fillAmount = progress;

            if (progress >= 0.9f)
            {
                progressBar.fillAmount = 1;
                astronaut.rectTransform.localPosition = new Vector3(-157 + 300, 25.5f, 0);
                
            }
            yield return new WaitForSeconds(.09f);
        }
        PhotonNetwork.LoadLevel(3);

    }
}