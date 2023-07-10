using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlanetBuilderLock : MonoBehaviour
{
    public static int amountOfObjects = 8;
    private GameObject[] objectList = new GameObject[amountOfObjects+1];
    private int lockState = 1;
    //public GameObject sliderObject;
    public Slider slider;
    private PhotonView view;
    // Start is called before the first frame update
    void Start()
    {
        //slider = sliderObject.GetComponent<Slider>();
        view = GetComponent<PhotonView>();
        for (int i = 0; i <= amountOfObjects; i++)
        {
            objectList[i] = this.gameObject.transform.GetChild(i).gameObject;
            objectList[i].gameObject.GetComponent<Button>().interactable = false;
        }

    }

    public void SliderChangeValue()
    {
        int value = (int)slider.value;
        if(!LobbyManager.userType)
            view.RPC("ChangeLock", RpcTarget.All, value);
    }

    public void LocalSliderChangeValue()
    {
        //ChangeLock((int)sliderObject.GetComponent<Slider>().value);
        ChangeLock((int)slider.value);
    }

    public void ChangeLock(int newLockState)
    {
        if (newLockState == lockState)
        {
            // Nothing happens :)
        }
        else if (newLockState > lockState)
        {
            // Increase Functionality 
            // Unlock what is not already unlocked, up to new State
            for (int i = lockState; i <= newLockState; i++)
            {
                objectList[i].gameObject.GetComponent<Button>().interactable = true; //Unlocked
            }

            lockState = newLockState; //Update State 
        }
        else 
        {
            // Decrease Functionality 
            // Lock what is not already locked 
            for (int i = lockState; i > newLockState; i--)
            {
                objectList[i].gameObject.GetComponent<Button>().interactable = false; //Locked 
            }
            lockState = newLockState; //Update State
        }
    }

}
