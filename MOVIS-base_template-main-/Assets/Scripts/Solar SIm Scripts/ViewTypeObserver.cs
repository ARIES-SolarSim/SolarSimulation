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

    public Text fact;
    public Text check;
    public Image astronaut;
    public Image rocket;
    public Image planet;
    public GameObject loaderCanvas;
    public Image progressBar;
    public FactData factList;
    public GameObject endScreen;
    public Button goBack;
    private int character;
    private bool pressedTwice = false;
    private int buttonNum;
    private PhotonView photonView;

    // public GameObject docentManager; 
    // Used for Changing the instance of DocentUI_Manager when ViewTypeObserver changes scenes
    // Notably, DocentUI_Manager is not in use. If that changes, uncomment the code! 

    private int currentViewType; //The current viewtype that the scene is displaying
    public static int targetViewType; //The view type that should be traveled to
    private int steps = 0; //Used for scene transitons

    private GameObject[] viewFinderCameras;
    public int view;

    public GameObject UICanvases;

    public RotateScript tempMoonRotate; // moon rotation script
    public MeshScaler tempMoonScale; // moon mesh script



    public static bool immediateTransition = false;
    

    public static TrailRenderer[] trails;
    
    private static string[] levelNames = { "Lobby", "Room1", "Room1", "Room2", "Room3", "Room1", "Room4" }; // names of each scene, in order (update as we add more)

    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        currentViewType = System.Array.IndexOf(levelNames, SceneManager.GetActiveScene().name);
        Debug.Log("Current view: " + currentViewType);

        // Start of application
        if (targetViewType < 1 || targetViewType > 6)
        {
            Debug.Log("Start");
            targetViewType = currentViewType;
        }

        // Switched to this scene and we're not in the right view
        else if (targetViewType != currentViewType)
        {
            Debug.Log("Setting target view type to " + targetViewType);
            transform.localPosition = new Vector3(targetViewType, 0, 0);
        }

        if (!UICanvases.activeInHierarchy && PhotonNetwork.NickName == "9")
            UICanvases.SetActive(true);

    }

    void Update()
    {


        
        int y = (int)transform.localPosition.y;

        // Switching to different views if the view we try to switch to is not the one we're in already
        if (y != currentViewType)
        {
            Debug.Log("Y:" + y + " | type: " + currentViewType);
            // Scene 1 has special cases
            if (y == 1)
            {

                LobbyManager.room = 1;
                // If not in room 1, go to it
                if (currentViewType > 2 && currentViewType != 6)
                {
                    if (PhotonNetwork.IsMasterClient)
                    {
                        PhotonView view = GetComponent<PhotonView>();
                        view.RPC("LoadingSomeLevel", RpcTarget.All, levelNames[1]);
                        //StartCoroutine(LoadingSomeLevel(levelNames[3]));

                    }


                }

                // If in room 1, toggle viewtype
                else
                {
                    targetViewType = 1;
                    transform.localPosition = new Vector3(1, 0, 0);

                }
                if (LobbyManager.room == 2)
                {
                    targetViewType = 2;
                    //currentViewType = 2;
                    transform.localPosition = new Vector3(2, 0, 0);
                }
                else if (LobbyManager.room == 5)
                {
                    targetViewType = 5;
                    //currentViewType = 5;
                    transform.localPosition = new Vector3(3, 0, 0);
                }
            }

            // Scene 2 has special cases
            else if (y == 2)
            {
                
                // If not in room 1, go to it, then immediately toggle to view 2
                // (does not work because new scene reloads everything. May need a separate
                // scene for going directly into view 2, but don't worry about that right now)
                if (currentViewType > 2 && currentViewType != 6)
                {
                    if (PhotonNetwork.IsMasterClient)
                    {
                        PhotonView view = GetComponent<PhotonView>();
                        view.RPC("LoadingSomeLevel", RpcTarget.All, levelNames[1]);
                        //StartCoroutine(LoadingSomeLevel(levelNames[3]));

                    }

                }

                // If in room 1, toggle
                else
                {
                    targetViewType = 2;

                    transform.localPosition = new Vector3(2, 0, 0);
                }
                LobbyManager.room = 2;

            }

            // Scene 6 has special cases
            else if (y == 3)
            {
                
                // If not in room 1, go to it, then immediately toggle to view 6
                // (does not work because new scene reloads everything. May need a separate
                // scene for going directly into view 6, but don't worry about that right now)
                //if (currentViewType > 2)
                //{
            
                if (PhotonNetwork.IsMasterClient)
                {


                    //Debug.Log("we are the master client");
                    PhotonView view = GetComponent<PhotonView>();
                    view.RPC("LoadingSomeLevel", RpcTarget.All, levelNames[3]);
                    //StartCoroutine(LoadingSomeLevel(levelNames[3]));

                }
                transform.localPosition = Vector3.zero;

                LobbyManager.room = 3;
                targetViewType = 3;
                view = 3;
                currentViewType = 3;
                //}

                // If in room 1, toggle
                /*else
                {
                    targetViewType = 6;
                    transform.localPosition = new Vector3(6, 0, 0);
                }*/
            }
            else if (y == 4)
            {
                

                if (PhotonNetwork.IsMasterClient)
                {
                    PhotonView view = GetComponent<PhotonView>();
                    view.RPC("LoadingSomeLevel", RpcTarget.All, levelNames[4]);
                    //StartCoroutine(LoadingSomeLevel(levelNames[3]));

                }


                //FindObjectOfType<UnityEngine.SpatialTracking.TrackedPoseDriver>().enabled = false;

                transform.localPosition = Vector3.zero;
                LobbyManager.room = 4;
                targetViewType = 4;
                view = 4;
                currentViewType = 4;

            }
            else if (y == 5) //Trivia -- NEW AND MIGHT BREAK 
            {
                

               // Debug.Log(currentViewType);
                if (currentViewType > 2 && currentViewType != 6)
                {
                    if (PhotonNetwork.IsMasterClient)
                    {
                        PhotonView view = GetComponent<PhotonView>();
                        view.RPC("LoadingSomeLevel", RpcTarget.All, levelNames[1]);
                        //StartCoroutine(LoadingSomeLevel(levelNames[3]));

                    }
                    targetViewType = 5;
                    transform.localPosition = Vector3.zero;
                }

                else
                {
                    targetViewType = 5;
                    transform.localPosition = new Vector3(3, 0, 0);

                }
                LobbyManager.room = 5;
                targetViewType = 5;
                view = 5;
                currentViewType = 5;
            }
            else if (y == 6) //Planet Builder -- NEW AND MIGHT BREAK 
            {
                targetViewType = 6;
                view = 6;
                currentViewType = 6;
                if (PhotonNetwork.IsMasterClient)
                {
                    PhotonView view = GetComponent<PhotonView>();
                    view.RPC("LoadingSomeLevel", RpcTarget.All, levelNames[6]);
                    //StartCoroutine(LoadingSomeLevel(levelNames[3]));
                }
            }
            else
            {
                Debug.Log("entered else");
                currentViewType = y;
                targetViewType = y; // Set this to avoid transition case
                transform.localPosition = Vector3.zero;


            }
        }

        // If trying to switch to the scene we're already in
        if (y == currentViewType)
        {
            transform.localPosition = Vector3.zero;
        }

        // Going between view 1, 2, and 6
        if (transform.localPosition.x != 0 && transform.localPosition.x != 6)
        {
            /*if (targetViewType != currentViewType)
            {
                transform.localPosition = Vector3.zero;
                steps = 0;
                FindObjectOfType<RotateScript>().view = targetViewType;
                FindObjectOfType<RotateScript>().changing = true;
            }*/
        }

        // Transitions between view 1, view 2, and view 6
        if (currentViewType != targetViewType && (LobbyManager.room == 1 || LobbyManager.room == 2))
        {
            
                transform.localPosition = new Vector3(targetViewType, 0, 1);
                tempMoonScale.changing();

                //Debug.Log("Mismatch");
                UniverseController.orbiting = false;
                FindObjectOfType<UniverseController>().gameObject.transform.localEulerAngles = Vector3.zero; //May need to become smooth

            
                if (steps == 0)
                {
                    foreach (PlanetController pc in FindObjectsOfType<PlanetController>())
                    {
                    Debug.Log("The targetviewtype is: " + targetViewType);
                    pc.changeViewType(targetViewType);
                    }
                }
                steps++;
                if (steps == UniverseController.changeDuration + 150)
                {

                    steps = 0; //Finished view type transistion
                    currentViewType = targetViewType;
                    transform.localPosition = new Vector3(0, 0, 0);
                    tempMoonScale.doneChanging();
                    Debug.Log("Finished transition");
                    if(LobbyManager.room == 1)
                    {
                    FindObjectOfType<UniverseController>().ArrowToggle(true);
                    }
                
                }
            
        }
    }

    public void PhotonChangeScene(int i)
    {
        if (pressedTwice & buttonNum == i)
        {
            PhotonView view = GetComponent<PhotonView>();
            view.RPC("changeScene", RpcTarget.MasterClient, i);
        }
        else
        {
            buttonNum = i;
            StartCoroutine(Check());
        }
        
    }



    IEnumerator Check()
    {
        check.gameObject.SetActive(true);
        pressedTwice = true;
        yield return new WaitForSeconds(5f);
        check.gameObject.SetActive(false);
        pressedTwice = false;
    }

    /**
    * Called by pressing the scene buttons on the docent UI tablet
    */
    [PunRPC]
    private void changeScene(int i)
    {

        PhotonView view = GetComponent<PhotonView>();
        // docentManager.GetComponent<DocentUI_Manager>().changeState((int)transform.localPosition.y, i);
        // Currently, DocentUI_Manager is unused, but if that changes, uncomment - Shane
        if (!LobbyManager.userType)
        {   
                transform.localPosition = new Vector3(0, i, 0);
 
        }
    }

    [PunRPC]
    IEnumerator LoadingSomeLevel(string sceneValue)
    {
        UICanvases.SetActive(false);
        loaderCanvas.SetActive(true);
        character = Random.Range(0, 2);
        int factInt = Random.Range(0, 21);
        fact.text = factList.FactList[factInt];
        

        Debug.Log("We are in loadingsomelevel");

        float progress = 0f;

        while (progress < 1f)
        {

            
                progress += 0.5f * 0.01f;
            
            
            
            if (character == 0)
            {
                rocket.enabled = false;
                planet.enabled = false;
                astronaut.enabled = true;

                astronaut.rectTransform.localPosition = new Vector3(-375 + (progress * 721), 75f, 0);
                progressBar.fillAmount = progress;

                if (progress >= 0.9f)
                {
                    progressBar.fillAmount = 1;
                    astronaut.rectTransform.localPosition = new Vector3(-375 + 721, 75f, 0);

                }
            }else if (character == 1)
            {
                astronaut.enabled = false;
                planet.enabled = false;
                rocket.enabled = true;

                rocket.rectTransform.localPosition = new Vector3(-320 + (progress * 721), 0, 0);
                progressBar.fillAmount = progress;

                if (progress >= 0.9f)
                {
                    progressBar.fillAmount = 1;
                    rocket.rectTransform.localPosition = new Vector3(-320 + 721, 0, 0);

                }
            }
            else if (character == 2)
            {
                astronaut.enabled = false;
                rocket.enabled = false;
                planet.enabled = true;

                planet.rectTransform.localPosition = new Vector3(-300 + (progress * 721), 0, 0);
                progressBar.fillAmount = progress;

                if (progress >= 0.9f)
                {
                    
                    progressBar.fillAmount = 1;
                    planet.rectTransform.localPosition = new Vector3(-300 + 721, 0, 0);
                    

                }
            }

            if (progress < 0.9f)
            {
                if (!LobbyManager.userType)
                {
                    yield return new WaitForSeconds(.03f);
                }
                else
                {
                    yield return new WaitForSeconds(.001f);
                }
            }
        }
        PhotonNetwork.LoadLevel(sceneValue);
        yield return new WaitForSeconds(2f);
    }

    public void endScene()
    {
        PhotonView view = GetComponent<PhotonView>();
        view.RPC("photonEndScene", RpcTarget.All);

    }

    [PunRPC]
    public void photonEndScene()
    {
        endScreen.SetActive(true);
        PhotonView view = GetComponent<PhotonView>();
        photonView = GetComponent<PhotonView>();
        if (photonView.Owner.NickName == "9" | photonView.Owner.NickName == "VR Headset Network Player")
        {
            goBack.gameObject.SetActive(true);
        }

    }

   

    public void returnScene()
    {
        PhotonView view = GetComponent<PhotonView>();
        view.RPC("photonReturnScene", RpcTarget.All);

    }

    [PunRPC]
    public void photonReturnScene()
    {
        endScreen.SetActive(false);
    }


}