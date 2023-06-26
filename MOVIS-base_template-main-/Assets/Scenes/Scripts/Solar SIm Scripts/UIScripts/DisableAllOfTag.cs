using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAllOfTag : MonoBehaviour
{
    public static void DisableAll(string str)
    {
        GameObject[] AllObjects = GameObject.FindGameObjectsWithTag(str);
        foreach (GameObject Object in AllObjects)
        {
            Object.SetActive(false);
        }
    }

}
