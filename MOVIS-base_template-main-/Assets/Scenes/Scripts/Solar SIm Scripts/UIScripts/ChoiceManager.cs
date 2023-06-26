using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceManager : MonoBehaviour
{
    public static int amountOfObjects = 8;
    private GameObject[] objectList = new GameObject[amountOfObjects];
    private int currentObject;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < amountOfObjects; i++)
        {
            objectList[i] = this.gameObject.transform.GetChild(i).gameObject;
        }

    }

    public void ChangeCurrentObject(int newObject)
    {
        objectList[currentObject].SetActive(false);
        currentObject = newObject;
        objectList[currentObject].SetActive(true);
    }
}
