using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlanetBuilderLock : MonoBehaviour
{
    public static int amountOfObjects = 8;
    private GameObject[] objectList = new GameObject[amountOfObjects];
    private int lockState = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < amountOfObjects; i++)
        {
            objectList[i] = this.gameObject.transform.GetChild(i).gameObject;
        }

        foreach (GameObject obj in objectList)
        {
            obj.gameObject.GetComponent<Button>().interactable = false; //Locked state applied immediately to all of the objects
        }
        objectList[0].gameObject.GetComponent<Button>().interactable = false; // Start with planet type

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
            for (int i = lockState; i >= newLockState; i--)
            {
                objectList[i].gameObject.GetComponent<Button>().interactable = false; //Locked 
            }
            lockState = newLockState; //Update State
        }
    }

}
