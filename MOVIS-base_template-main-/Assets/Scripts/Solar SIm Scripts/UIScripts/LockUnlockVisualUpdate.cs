using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockUnlockVisualUpdate : MonoBehaviour
{
    public GameObject lockVisual;
    public GameObject unlockVisual;
    private bool locked; //True is locked 
    public void UpdateState()
    {
        if (locked)
        {
            lockVisual.gameObject.SetActive(false);
            unlockVisual.gameObject.SetActive(true);
            locked = false; 
        }
        else
        {
            lockVisual.gameObject.SetActive(true);
            unlockVisual.gameObject.SetActive(false);
            locked = true;
        }
    }
}
