using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR
{
    public class SteamVRMenuItem : MonoBehaviour
    {
        public float maxRot = 135f;
        public int TypeID; //0 = Knob, 1 = 3D Rotational Object 
        public int ObjectID; //Specific to the exact Object
        public SteamVRController controller; //The script that created this menu item
        public bool isCollide = false;
        public Vector3 referenceRotation;
        public Vector3 referenceLocation;
        private float maxPos = 0.3f;

        public Vector3 currentRot;
        public Vector3 currentPos;

        public enum States { ENTER, FOLLOW, NOTHING }
        public States curState = States.NOTHING;
        public void construct(int objID, int tyID, SteamVRController con)
        {
            ObjectID = objID;
            TypeID = tyID;
            controller = con;
        }

        // Update is called once per frame
        void Update()
        {
            switch (curState)
            {
                case States.ENTER:
                    if (controller.RightTrigger.GetAxis(SteamVR_Input_Sources.Any) > 0.9f || (controller.LeftTrigger.GetAxis(SteamVR_Input_Sources.Any) > 0.9f))
                    {
                        referenceRotation = controller.RightHandPos.transform.eulerAngles;
                        referenceLocation = controller.RightHandPos.transform.localPosition;
                        currentRot = transform.eulerAngles;
                        currentPos = transform.localPosition;
                        curState = States.FOLLOW;
                    }
                    if (!isCollide)
                    {
                        curState = States.NOTHING;
                    }
                    break;
                case States.FOLLOW:
                    if (controller.RightTrigger.GetAxis(SteamVR_Input_Sources.Any) < 0.1f && (controller.LeftTrigger.GetAxis(SteamVR_Input_Sources.Any) < 0.1f))
                    {
                        curState = States.NOTHING;
                    }
                    break;
                case States.NOTHING:
                    if (isCollide)
                    {
                        curState = States.ENTER;
                    }
                    break;
            }
            if (TypeID == 0)
            {
                if(ObjectID == 0) //OrbitSpeedK
                {
                    float slope = 20f / (2 * maxPos); //Should allow for the speed to be between 0 and 20
                    if(curState == States.FOLLOW)
                    {
                        /* There is a problem where when the controller goes past 180 degrees it jumps to -180 which messes with things, fix it another day
                        Vector3 temp = new Vector3(0f, Mathf.Min(Mathf.Max(controller.RightHandPos.transform.eulerAngles.y - referenceRotation.y, -1 * maxRot), maxRot), 0f); //Sets to 0 beyond 360, find a fix
                        Debug.Log(controller.RightHandPos.transform.eulerAngles.y + " " + referenceRotation.y + " " + temp.y);
                        transform.eulerAngles = temp;
                        */
                        Debug.Log((controller.RightHandPos.transform.localPosition - referenceLocation - currentPos).x);
                        transform.localPosition = new Vector3(Mathf.Min(Mathf.Max(-maxPos, (controller.RightHandPos.transform.localPosition - referenceLocation - currentPos).x), maxPos), transform.localPosition.y, transform.localPosition.z);
                    }
                    float xTemp = transform.localPosition.x * slope + 10f;
                    float yTemp = controller.MenuItemNetworker.transform.localPosition.y;
                    float zTemp = controller.MenuItemNetworker.transform.localPosition.z;
                    //controller.MenuItemNetworker.transform.localPosition = new Vector3(xTemp, yTemp, zTemp); //X: speed Y: NA Z: NA
                }
            }
            if(TypeID == 1)
            {
                if(ObjectID == 0) //Universe Center Rotator, mainly for viewtype 3
                {
                    if(curState == States.FOLLOW)
                    {
                        //Debug.Log((controller.RightHandPos.transform.eulerAngles - referenceRotation - currentRot).magnitude + " " + (controller.RightHandPos.transform.eulerAngles - referenceRotation - currentRot));
                        transform.eulerAngles = ((controller.RightHandPos.transform.eulerAngles - referenceRotation - currentRot).magnitude <= 0.1f) ? new Vector3(0, 0, 0) : controller.RightHandPos.transform.eulerAngles - referenceRotation - currentRot;
                        controller.MenuItemNetworker.transform.eulerAngles = transform.eulerAngles; //X, Y, Z universe rotation

                        //Anti-jitter not working
                    }
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            isCollide = true;
        }

        private void OnTriggerExit(Collider other)
        {
            isCollide = false;
        }
    }
}
