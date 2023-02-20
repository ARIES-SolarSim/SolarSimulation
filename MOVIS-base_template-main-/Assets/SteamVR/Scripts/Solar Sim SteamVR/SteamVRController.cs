using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Any script that needs to run directly with the VR system needs to operate in the Valve.VR namespace
 */
namespace Valve.VR
{
    /*
     * This object is attached to the parent object of the controllers. It is responsible for taking any and all inputs from the controllers and
     * doing the correct task depending on the input
     */
    public class SteamVRController : MonoBehaviour 
    {
        public float sensitivity; //How sensitive the trigger is for moving - DEVELOPER USE ONLY
        public float maxSpeed; //How fast the movement is - DEVELOPER USE ONLY

        public SteamVR_Action_Boolean LeftButton; //The action to change viewtypes on the left controller
        public SteamVR_Action_Boolean RightButton; //The action to change viewtypes on the right controller
        public SteamVR_Action_Single RightTrigger; //The action related to pulling the trigger. Currently used for movement - DEVELOPER USE ONLY
        public SteamVR_Action_Single LeftTrigger; //The action related to pulling the trigger. Currently used for movement - DEVELOPER USE ONLY

        public GameObject LeftHandPos; //The object with the current location of the left controller
        public GameObject RightHandPos; //The object with the current location of the right controller

        public GameObject LeftHandCol; //The object with the collider of the left controller
        public GameObject RightHandCol; //The object with the collider of the right controller

        public GameObject ViewTypeMenuPrefab; //The prefab for the menu object
        public GameObject ViewTypeNetworker; //The network object used for changing scenes

        public GameObject[] MenuItems;
        public GameObject MenuItemNetworker;

        private Vector3 speed = Vector3.zero;

        public bool[] leftChanging = { false, false }; //[0] is if the menu is open, [1] is if the button is pressed
        public bool[] rightChanging = { false, false };

        /*public int leftButtonState = 0;
        public int rightButtonState = 0;*/
        //0 Means pressing will open menu, 1 Means currently pressed showing menu, 2 means unpressed with next press removing items, 3 means pressed and removing items
        public bool isChanging = false;

        private int ViewTypeCount = 2;//If adding a viewtype, change this value AND the one in PlanetData
        private GameObject[] deleteList; //An array of the menu objects created while in use.

        public int leftControllerRotationCount = 0;
        public int rightControllerRotationCount = 0;
        private float leftPreviousRot = 0f;
        private float rightPreviousRot = 0f;

        //Start method
        private void Start()
        {
            deleteList = new GameObject[Mathf.Max(ViewTypeCount, MenuItems.Length)]; 
        }

        //Upate method
        private void Update()
        {
            CalcMovement();//Only for testing, remove for demo.
            CheckMenu();
        }

        /*
         * When called in the update method, it checks if the controller viewtype buttons are down.
         * If that is the case, it creates the menu items around the corresponding controller.
         */
        private void CheckMenu()
        {
            if((LeftButton.state || RightButton.state) && !isChanging)
            {
                isChanging = true;
                ViewTypeNetworker.transform.localPosition = new Vector3(1, ViewTypeNetworker.transform.localPosition.y, ViewTypeNetworker.transform.localPosition.z);
            }
            /*
            if(LeftButton.state && leftButtonState == 0 && rightButtonState == 0) //Left Hand Changes Viewtype
            {
                leftButtonState = 1;
                for (int i = 0; i < ViewTypeCount; i ++)
                {
                    Vector3 LeftHandRot = LeftHandPos.transform.rotation.eulerAngles;
                    LeftHandRot.x = 0f;
                    LeftHandRot.z = 0f;
                    GameObject view = Instantiate(ViewTypeMenuPrefab, LeftHandPos.transform.position + Quaternion.Euler(LeftHandRot) * (new Vector3(i*0.2f - 0.2f, 0.1f, 0.2f)), transform.rotation);
                    view.GetComponent<SteamVRViewTypeMenu>().ViewTypeID = i + 1;
                    view.GetComponent<SteamVRViewTypeMenu>().controller = this;
                    deleteList[i] = view;
                }
            }
            if(leftButtonState == 1 && !LeftButton.state)
            {
                leftButtonState = 2;
            }
            if(leftButtonState == 2 && LeftButton.state)
            {
                removeMenu();
                leftButtonState = 3;
            }
            if (leftButtonState == 3 && !LeftButton.state)
            {
                leftButtonState = 0;
            } //Make this into enum states at some point
            */
            /*
            else if (RightButton.state && leftButtonState == 0 && rightButtonState == 0) //Right Hand opens other menu items
            {
                Vector3 RightHandRot = RightHandPos.transform.rotation.eulerAngles;
                RightHandRot.x = 0f;
                RightHandRot.z = 0f;
                rightButtonState = 1;
                GameObject menu1 = Instantiate(MenuItems[0], RightHandPos.transform.position + Quaternion.Euler(RightHandRot) * (new Vector3( -0.2f, 0.1f, 0)), Quaternion.Euler(new Vector3(0, 0, 0)));
                GameObject menu2 = Instantiate(MenuItems[1], RightHandPos.transform.position + Quaternion.Euler(RightHandRot) * (new Vector3(0.2f, 0.1f, 0)), Quaternion.Euler(new Vector3(0, 0, 0)));
                menu1.GetComponent<SteamVRMenuItem>().construct(0, 0, this);
                menu2.GetComponent<SteamVRMenuItem>().construct(0, 1, this);
                deleteList[0] = menu1;
                deleteList[1] = menu2;
            }
            if (rightButtonState == 1 && !RightButton.state)
            {
                rightButtonState = 2;
            }
            if (rightButtonState == 2 && RightButton.state)
            {
                removeMenu();
                rightButtonState = 3;
            }
            if (rightButtonState == 3 && !RightButton.state)
            {
                rightButtonState = 0;
            } //Make this into enum states at some point*/
        }

        public void removeMenu()
        {
            foreach (GameObject go in deleteList)
            {
                Destroy(go);
            }
        }

        /*
         * Moves the VR user when either of the triggers have been pulled.
         * Note that the user moves in the direction that the controller is pointing. 
         * DEVELOPER USE ONLY
         */ 
        private void CalcMovement()
        {
            if (LeftTrigger.GetAxis(SteamVR_Input_Sources.Any) > sensitivity)
            {
                speed = LeftHandPos.transform.rotation * Vector3.forward * maxSpeed * LeftTrigger.GetAxis(SteamVR_Input_Sources.Any);
                speed = new Vector3(speed.x, 0, speed.z);
            }
            else if (RightTrigger.GetAxis(SteamVR_Input_Sources.Any) > sensitivity)
            {
                speed = RightHandPos.transform.rotation * Vector3.forward * maxSpeed * RightTrigger.GetAxis(SteamVR_Input_Sources.Any);
                speed = new Vector3(speed.x, 0, speed.z);
            }
            else
            {
                speed = Vector3.zero;
            }
            transform.localPosition += speed;
        }

    }

}