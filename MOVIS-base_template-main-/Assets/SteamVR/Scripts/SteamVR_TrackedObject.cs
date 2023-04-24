//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: For controlling in-game objects with tracked devices.
//
//=============================================================================

using UnityEngine;
using Valve.VR;


namespace Valve.VR
{
    public class SteamVR_TrackedObject : MonoBehaviour
    {
        public enum EIndex
        {
            None = -1,
            Hmd = (int)OpenVR.k_unTrackedDeviceIndex_Hmd,
            Device1,
            Device2,
            Device3,
            Device4,
            Device5,
            Device6,
            Device7,
            Device8,
            Device9,
            Device10,
            Device11,
            Device12,
            Device13,
            Device14,
            Device15,
            Device16
        }


        public EIndex index;

        [Tooltip("If not set, relative to parent")]
        public Transform origin;

        public bool isValid { get; private set; }

        private void OnNewPoses(TrackedDevicePose_t[] poses)
        {
            if (index == EIndex.None)
                return;

            var i = (int)index;

            isValid = false;
            if (poses.Length <= i)
                return;

            if (!poses[i].bDeviceIsConnected)
                return;

            if (!poses[i].bPoseIsValid)
                return;

            isValid = true;

            var pose = new SteamVR_Utils.RigidTransform(poses[i].mDeviceToAbsoluteTracking);

            if (origin != null)
            {
                transform.position = origin.transform.TransformPoint(pose.pos);
                transform.rotation = origin.rotation * pose.rot;
            }
            else
            {
                transform.localPosition = pose.pos;
                transform.localRotation = pose.rot;
            }
        }

        SteamVR_Events.Action newPosesAction;

        SteamVR_TrackedObject()
        {
            newPosesAction = SteamVR_Events.NewPosesAction(OnNewPoses);
        }

        private void Awake()
        {
            OnEnable();
        }

        void OnEnable()
        {
            var render = SteamVR_Render.instance;
            if (render == null)
            {
                enabled = false;
                return;
            }

            newPosesAction.enabled = true;
        }

        void OnDisable()
        {
            newPosesAction.enabled = false;
            isValid = false;
        }

        public void SetDeviceIndex(int index)
        {
            if (System.Enum.IsDefined(typeof(EIndex), index))
                this.index = (EIndex)index;
        }

        #region TrackerAutoMaticSetting

        //DO NOT FIX THIS UNLESS YOU ARE REPLACING THE TRACKERS!!!!

        [Tooltip("If you want to use the trackers specified in the code, check this to true")]
        public bool hardCodeTrackers = true;

        [Tooltip("Only if the trackers are hard coded, this enum will hard code the trackers" +
            "to the current system, depending on what system it is using specified from input" +
            "This means that this can change at the instantiation scene or in the editor not in play but not live")]
        public CurrentSystem currentSystem;

        public string Tracker1ID = "LHR-E66AE7F2";
        public string Tracker2ID = "LHR-0826191F";
        public string Tracker3ID = "LHR-6C7536DB";
        public string Tracker4ID = "LHR-225139C6";
        public string Tracker5ID = "LHR-7F7C9F3E";
        public string Tracker6ID = "LHR-C57C3B1A";
        public string Tracker7ID = "LHR-6B3E4E5C";
        public string Tracker8ID = "LHR-70CC7F0F";
        public string Tracker9ID = "LHR-88D33ED6";

        private void Start()
        {
            TrackerSetup();
        }

        void TrackerSetup()
        {
            if (hardCodeTrackers)
            {
                switch (currentSystem)
                {
                    case CurrentSystem.ARIES:
                        //Do nothing, default
                        break;
                    case CurrentSystem.MOVIS:
                        Tracker1ID = "LHR-A36C3E16";
                        Tracker2ID = "LHR-4780977C";
                        Tracker3ID = "LHR-4F225E25";
                        Tracker4ID = "LHR-B664C378";
                        Tracker5ID = "LHR-A43DC925";
                        Tracker6ID = "LHR-69199DDE";
                        Tracker7ID = "LHR-9EF9F428";
                        Tracker8ID = "LHR-2430F038";
                        Tracker9ID = "LHR-C54B6AFF";
                        break;
                    case CurrentSystem.MUSUEM:
                        Tracker1ID = "LHR-A36C3E16";
                        Tracker2ID = "LHR-4780977C";
                        Tracker3ID = "LHR-4F225E25";
                        Tracker4ID = "LHR-B664C378";
                        Tracker5ID = "LHR-A43DC925";
                        Tracker6ID = "LHR-69199DDE";
                        Tracker7ID = "LHR-9EF9F428";
                        Tracker8ID = "LHR-2430F038";
                        Tracker9ID = "LHR-C54B6AFF";
                        break;

                }
            }



            ETrackedPropertyError error = new ETrackedPropertyError();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            for (int i = 0; i <= 16; i++) //check all device from 0 - 16;
            {
                OpenVR.System.GetStringTrackedDeviceProperty((uint)i, ETrackedDeviceProperty.Prop_SerialNumber_String, sb, OpenVR.k_unMaxPropertyStringSize, ref error);
                var SerialNumber = sb.ToString(); //it will extract the tracker number.

                //Below is where you need to identify the tracker and setup.

                if (SerialNumber == Tracker1ID) //if extracted traker number is matched with the TrakcerID,
                {
                    if (this.gameObject.name.Contains("1")) //AND, it matches with prefab ID,
                    {
                        SetDeviceIndex(i); //Set to designated device number.
                        break;
                    }
                }

                if (SerialNumber == Tracker2ID) //if extracted traker number is matched with the TrakcerID,
                {
                    if (this.gameObject.name.Contains("2")) //AND, it matches with prefab ID,
                    {
                        SetDeviceIndex(i); //Set to designated device number.
                        break;
                    }
                }

                if (SerialNumber == Tracker3ID) //if extracted traker number is matched with the TrakcerID,
                {
                    if (this.gameObject.name.Contains("3")) //AND, it matches with prefab ID,
                    {
                        SetDeviceIndex(i); //Set to designated device number.
                        break;
                    }
                }

                if (SerialNumber == Tracker4ID) //if extracted traker number is matched with the TrakcerID,
                {
                    if (this.gameObject.name.Contains("4")) //AND, it matches with prefab ID,
                    {
                        SetDeviceIndex(i); //Set to designated device number.
                        break;
                    }
                }

                if (SerialNumber == Tracker5ID) //if extracted traker number is matched with the TrakcerID,
                {
                    if (this.gameObject.name.Contains("5")) //AND, it matches with prefab ID,
                    {
                        SetDeviceIndex(i); //Set to designated device number.
                        break;
                    }
                }

                if (SerialNumber == Tracker6ID) //if extracted traker number is matched with the TrakcerID,
                {
                    if (this.gameObject.name.Contains("6")) //AND, it matches with prefab ID,
                    {
                        SetDeviceIndex(i); //Set to designated device number.
                        break;
                    }
                }

                if (SerialNumber == Tracker7ID) //if extracted traker number is matched with the TrakcerID,
                {
                    if (this.gameObject.name.Contains("7")) //AND, it matches with prefab ID,
                    {
                        SetDeviceIndex(i); //Set to designated device number.
                        break;
                    }
                }

                if (SerialNumber == Tracker8ID)
                {
                    if (this.gameObject.name.Contains("8"))
                    {
                        SetDeviceIndex(i);
                        break;
                    }
                }
                //if (SerialNumber == Tracker9ID)
                // {
                //if (this.gameObject.name.Contains("9"))
                //{
                //SetDeviceIndex(i);
                //break;
                //}
                //}


            }
        }
        #endregion
    }



}

public enum CurrentSystem
{
    MOVIS,
    ARIES,
    MUSUEM
}