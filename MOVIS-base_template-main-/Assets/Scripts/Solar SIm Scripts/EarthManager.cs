using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthManager : MonoBehaviour
{
    public void toggleVisability(bool isShown)
    {
        this.gameObject.SetActive(isShown);
    }
}
