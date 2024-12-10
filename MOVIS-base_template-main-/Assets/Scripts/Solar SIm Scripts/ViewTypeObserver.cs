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
    private int holdY;

    // public GameObject docentManager; 
    // Used for Changing the instance of DocentUI_Manager when ViewTypeObserver changes scenes
    // Notably, DocentUI_Manager is not in use. If that changes, uncomment the code! 

    private int currentViewType; //The current viewtype that the scene is displaying
    public static int targetViewType; //The view type that should be traveled to
    private int steps = -1; //Used for scene transitons

    private GameObject[] viewFinderCameras;
    public int view;

    public GameObject UICanvases;
    public GameObject playerCanvas;

    public RotateScript tempMoonRotate; // moon rotation script
    public MeshScaler tempMoonScale; // moon mesh script
    public static int tempVal;


    public static bool immediateTransition = false;


    public static TrailRenderer[] trails;

    private static string[] levelNames = { "Lobby", "Room1", "Room1", "Room2", "Room3", "Room5", "Room4" }; // names of each scene, in order (update as we add more)
    private static string[] levelNamesPC = { "Lobby", "Room1PC", "Room1PC", "Room2PC", "Room3PC", "Room5PC", "Room4PC" }; // names of each scene, in order (update as we add more)

    public bool isOnPc;

    public int pcTargetView = 0;

    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        currentViewType = System.Array.IndexOf(levelNames, SceneManager.GetActiveScene().name);


        if (targetViewType > 0)
        {
            LobbyManager.room = targetViewType;
        }


        // Start of application
        if (targetViewType < 1 || targetViewType > 6)
        {
            //Debug.Log("Start");
            targetViewType = currentViewType;
        }

        // Switched to this scene and we're not in the right view
        else if (targetViewType != currentViewType)
        {


            transform.localPosition = new Vector3(0, targetViewType, 1);
        }

        if(!isOnPc)
        {
            if (PhotonNetwork.NickName == "9" || !LobbyManager.userType)
            {
                UICanvases.SetActive(true);
            }


            view = targetViewType;

            if (PhotonNetwork.NickName != "9" && view == 6)
            {
                playerCanvas.SetActive(true);
            }
        }
    }

    public void setPCTargetView(int i)
    {
        if (pressedTwice & buttonNum == i)
        {
            pcTargetView = i;
        }
        else
        {
            buttonNum = i;
            StartCoroutine(Check());
        }
    }

    void Update()
    {
        if (targetViewType != 0)
        {
            tempVal = targetViewType;
        }
        if (isOnPc)
        {
            if (pcTargetView != currentViewType)
            {
                // Scene 1 has special cases
                if (pcTargetView == 1)
                {
                    // If not in room 1, go to it
                    if (!LobbyManager.room1)
                    {
                        PCLoadingSomeLevel(levelNamesPC[1]);
                        LobbyManager.room1 = true;
                    }

                    // If in room 1, toggle viewtype
                    else
                    {
                        if (LobbyManager.room < 3 || LobbyManager.room == 5)
                        {
                            LobbyManager.room = 1;
                        }
                        Debug.Log("Setting steps to 0 FIRST");
                        steps = 0;
                        currentViewType = 1;
                        transform.localPosition = new Vector3(1, 0, 0);
                    }
                }

                // Scene 2 has special cases
                else if (pcTargetView == 2)
                {
                    // If not in room 1, go to it, then immediately toggle to view 2
                    // (does not work because new scene reloads everything. May need a separate
                    // scene for going directly into view 2, but don't worry about that right now)
                    if (!LobbyManager.room1)
                    {
                        PCLoadingSomeLevel(levelNamesPC[1]);
                        pcTargetView = 2;
                    }
                    // If in room 1, toggle
                    else
                    {
                        if (LobbyManager.room == 1)
                        {
                            LobbyManager.room = 2;
                        }
                        Debug.Log("Setting steps to 0 SECOND");
                        steps = 0;
                        currentViewType = 2;

                        transform.localPosition = new Vector3(2, 0, 1);
                    }
                    LobbyManager.room1 = true;
                }

                // Scene 6 has special cases
                else if (pcTargetView == 3)
                {
                    Debug.Log("Test2");
                    steps = -1;
                    transform.localPosition = Vector3.zero;
                    LobbyManager.room1 = false;
                    LobbyManager.room = 3;
                    pcTargetView = 3;
                    view = 3;
                    currentViewType = 3;
                    PCLoadingSomeLevel(levelNamesPC[3]);
                }
                else if (pcTargetView == 4)
                {
                    Debug.Log("Test3");
                    steps = -1;
                    transform.localPosition = Vector3.zero;
                    LobbyManager.room1 = false;
                    LobbyManager.room = 4;
                    pcTargetView = 4;
                    view = 4;
                    currentViewType = 4;
                    PCLoadingSomeLevel(levelNames[4]);
                }
                else if (pcTargetView == 5) //Trivia -- NEW AND MIGHT BREAK 
                {
                    Debug.Log("Test4");
                    steps = -1;
                    transform.localPosition = Vector3.zero;
                    LobbyManager.room1 = false;
                    LobbyManager.room = 5;
                    pcTargetView = 5;
                    view = 5;
                    currentViewType = 5;
                    PCLoadingSomeLevel(levelNamesPC[5]);
                }
                else if (pcTargetView == 6) //Planet Builder -- NEW AND MIGHT BREAK 
                {
                    Debug.Log("Test5");
                    steps = -1;
                    transform.localPosition = Vector3.zero;
                    LobbyManager.room1 = false;
                    LobbyManager.room = 6;
                    pcTargetView = 6;
                    view = 6;
                    currentViewType = 6;
                    PCLoadingSomeLevel(levelNamesPC[6]);
                }
                else
                {
                    currentViewType = pcTargetView;
                    targetViewType = pcTargetView;
                }
            }
            //Debug.Log("Before: " + steps);
            // Transitions between view 1, view 2, and view 6
            if (LobbyManager.room == 1 || LobbyManager.room == 2)
            {
                //Debug.Log(steps);
                if (steps > -1)
                {
                    transform.localPosition = new Vector3(pcTargetView, 0, 1);
                    if(tempMoonScale != null)
                        tempMoonScale.changing();
                    UniverseController.orbiting = false;
                    FindObjectOfType<UniverseController>().gameObject.transform.localEulerAngles = Vector3.zero; //May need to become 
                    if (steps == 0)
                    {
                        FindObjectOfType<RotateScript>().changing = true;
                        foreach (PlanetController pc in FindObjectsOfType<PlanetController>())
                        {
                            Debug.Log("PC Change View Type");
                            if (pcTargetView == 5)
                            {
                                pc.changeViewType(3);
                            }
                            else
                            {
                                pc.changeViewType(pcTargetView);
                            }
                        }
                    }
                    steps++;
                }
                if (steps == (UniverseController.changeDuration + UniverseController.accDuration))
                {
                    //Debug.Log("huh");
                    FindObjectOfType<RotateScript>().changing = false;
                    MeshScaler.view = (MeshScaler.view == 1) ? 0 : 1;
                    FindObjectOfType<RotateScript>().view = LobbyManager.room;
                    steps = -1; //Finished view type transistion
                    currentViewType = pcTargetView;
                    transform.localPosition = new Vector3(0, 0, 0);
                    tempMoonScale.doneChanging();

                    if (LobbyManager.room == 1)
                    {
                        FindObjectOfType<UniverseController>().ArrowToggle(true);
                    }

                    if (LobbyManager.room == 2)
                    {
                        FindObjectOfType<UniverseController>().ArrowToggle(false);
                    }
                }
            }
            //Debug.Log("After: " + steps);
        }
        else
        {
            int y = (int)transform.localPosition.y;
            // Switching to different views if the view we try to switch to is not the one we're in already
            if (y != currentViewType)
            {
                // Scene 1 has special cases
                if (y == 1)
                {
                    // If not in room 1, go to it
                    if (!LobbyManager.room1)
                    {
                        if (PhotonNetwork.IsMasterClient)
                        {
                            PhotonView view = GetComponent<PhotonView>();
                            view.RPC("LoadingSomeLevel", RpcTarget.All, levelNames[1]);
                        }
                        LobbyManager.room1 = true;
                    }

                    // If in room 1, toggle viewtype
                    else
                    {
                        if (LobbyManager.room < 3 || LobbyManager.room == 5)
                        {
                            LobbyManager.room = 1;
                        }
                        steps = 0;
                        Debug.Log("Setting steps to 0 THIRD");
                        targetViewType = 1;
                        transform.localPosition = new Vector3(1, 0, 0);
                    }
                }

                // Scene 2 has special cases
                else if (y == 2)
                {
                    // If not in room 1, go to it, then immediately toggle to view 2
                    // (does not work because new scene reloads everything. May need a separate
                    // scene for going directly into view 2, but don't worry about that right now)
                    if (!LobbyManager.room1)
                    {
                        if (PhotonNetwork.IsMasterClient)
                        {
                            PhotonView view = GetComponent<PhotonView>();
                            view.RPC("LoadingSomeLevel", RpcTarget.All, levelNames[1]);
                            targetViewType = 2;
                        }

                    }
                    // If in room 1, toggle
                    else
                    {
                        if (LobbyManager.room == 1)
                        {
                            LobbyManager.room = 2;
                        }
                        Debug.Log("Setting steps to 0 FOURTH");
                        steps = 0;
                        targetViewType = 2;

                        transform.localPosition = new Vector3(2, 0, 1);
                    }
                    LobbyManager.room1 = true;
                }

                // Scene 6 has special cases
                else if (y == 3)
                {
                    // If not in room 1, go to it, then immediately toggle to view 6
                    // (does not work because new scene reloads everything. May need a separate
                    // scene for going directly into view 6, but don't worry about that right now)
                    //if (currentViewType > 2)                                                                                                                             
                    transform.localPosition = Vector3.zero;
                    LobbyManager.room1 = false;
                    LobbyManager.room = 3;
                    targetViewType = 3;
                    view = 3;
                    currentViewType = 3;
                    if (PhotonNetwork.IsMasterClient)
                    {
                        //Debug.Log("we are the master client");
                        PhotonView view = GetComponent<PhotonView>();
                        view.RPC("LoadingSomeLevel", RpcTarget.All, levelNames[3]);
                        //StartCoroutine(LoadingSomeLevel(levelNames[3]));
                    }
                    // If in room 1, toggle
                    /*else
                    {
                        targetViewType = 6;
                        transform.localPosition = new Vector3(6, 0, 0);
                    }*/
                }
                else if (y == 4)
                {
                    steps = -1;
                    transform.localPosition = Vector3.zero;
                    LobbyManager.room1 = false;
                    LobbyManager.room = 4;
                    targetViewType = 4;
                    view = 4;
                    currentViewType = 4;
                    if (PhotonNetwork.IsMasterClient)
                    {
                        PhotonView view = GetComponent<PhotonView>();
                        view.RPC("LoadingSomeLevel", RpcTarget.All, levelNames[4]);
                    }
                }
                else if (y == 5) //Trivia -- NEW AND MIGHT BREAK 
                {
                    steps = -1;
                    transform.localPosition = Vector3.zero;
                    LobbyManager.room1 = false;
                    LobbyManager.room = 5;
                    targetViewType = 5;
                    view = 5;
                    currentViewType = 5;
                    if (PhotonNetwork.IsMasterClient)
                    {
                        PhotonView view = GetComponent<PhotonView>();
                        view.RPC("LoadingSomeLevel", RpcTarget.All, levelNames[5]);
                    }
                    /*
                    // If not in room 1, go to it, then immediately toggle to view 2
                    // (does not work because new scene reloads everything. May need a separate
                    // scene for going directly into view 2, but don't worry about that right now)
                    if (!LobbyManager.room1)
                    {
                        if (PhotonNetwork.IsMasterClient)
                        {
                            PhotonView view = GetComponent<PhotonView>();
                            view.RPC("LoadingSomeLevel", RpcTarget.All, levelNames[5]);
                            targetViewType = 5;
                        }
                    }
                    // If in room 1, toggle
                    else
                    {
                        if (LobbyManager.room == 1)
                        {
                            LobbyManager.room = 5;
                        }
                        steps = 0;
                        targetViewType = 5;
                        transform.localPosition = new Vector3(5, 0, 1);
                    }
                    LobbyManager.room1 = true;
                    */
                }
                else if (y == 6) //Planet Builder -- NEW AND MIGHT BREAK 
                {
                    steps = -1;
                    transform.localPosition = Vector3.zero;
                    LobbyManager.room1 = false;
                    LobbyManager.room = 6;
                    targetViewType = 6;
                    view = 6;
                    currentViewType = 6;
                    if (PhotonNetwork.IsMasterClient)
                    {
                        PhotonView view = GetComponent<PhotonView>();
                        view.RPC("LoadingSomeLevel", RpcTarget.All, levelNames[6]);
                    }
                }
                else
                {
                    currentViewType = y;
                    targetViewType = y; // Set this to avoid transition case
                }
            }
            // If trying to switch to the scene we're already in
            /*if (y == currentViewType)
            {
                transform.localPosition = Vector3.zero;
                Debug.Log("Y= current & y: " + y + " | Current: " + currentViewType + " | target: " + targetViewType);
            }*/

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
            if (LobbyManager.room == 1 || LobbyManager.room == 2)
            {
                if (steps > -1)
                {
                    transform.localPosition = new Vector3(targetViewType, 0, 1);
                    tempMoonScale.changing();
                    UniverseController.orbiting = false;
                    FindObjectOfType<UniverseController>().gameObject.transform.localEulerAngles = Vector3.zero; //May need to become 
                    if (steps == 0)
                    {
                        FindObjectOfType<RotateScript>().changing = true;
                        foreach (PlanetController pc in FindObjectsOfType<PlanetController>())
                        {
                            Debug.Log("NOT PC Change View Type");
                            if (targetViewType == 5)
                            {
                                pc.changeViewType(3);
                            }
                            else
                            {
                                pc.changeViewType(targetViewType);
                            }
                        }
                    }
                    steps++;
                }
                if (steps == (UniverseController.changeDuration + UniverseController.accDuration))
                {
                    Debug.Log("Change Complete");
                    FindObjectOfType<RotateScript>().changing = false;
                    MeshScaler.view = (MeshScaler.view == 1) ? 0 : 1;
                    FindObjectOfType<RotateScript>().view = LobbyManager.room;
                    steps = -1; //Finished view type transistion
                    currentViewType = targetViewType;
                    transform.localPosition = new Vector3(0, 0, 0);
                    tempMoonScale.doneChanging();

                    if (LobbyManager.room == 1)
                    {
                        FindObjectOfType<UniverseController>().ArrowToggle(true);
                    }

                    if (LobbyManager.room == 2)
                    {
                        FindObjectOfType<UniverseController>().ArrowToggle(false);
                    }
                }
            }
        }
    }

    public void PhotonChangeScene(int i)
    {
        if (!isOnPc)
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
        //PhotonView view = GetComponent<PhotonView>();
        // docentManager.GetComponent<DocentUI_Manager>().changeState((int)transform.localPosition.y, i);
        // Currently, DocentUI_Manager is unused, but if that changes, uncomment - Shane
        if (!LobbyManager.userType)
        {
            if (LobbyManager.room != i)
            {
                transform.localPosition = new Vector3(0, i, 0);
            }
        }
    }

    //PC LoadLevel
    [PunRPC]
    IEnumerator PCLoadingSomeLevel(string sceneValue)
    {
        UICanvases.SetActive(false);
        loaderCanvas.SetActive(true);
        character = Random.Range(0, 3);
        int factInt = Random.Range(0, 21);
        fact.text = factList.FactList[factInt];

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
            }
            else if (character == 1)
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

    [PunRPC]
    IEnumerator LoadingSomeLevel(string sceneValue)
    {
        UICanvases.SetActive(false);
        loaderCanvas.SetActive(true);
        character = Random.Range(0, 3);
        int factInt = Random.Range(0, 21);
        fact.text = factList.FactList[factInt];

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
            }
            else if (character == 1)
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
        if (photonView.Owner.NickName == "9" || photonView.Owner.NickName == "VR Headset Network Player")
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