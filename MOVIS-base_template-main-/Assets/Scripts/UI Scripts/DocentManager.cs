using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocentManager : MonoBehaviour
{
    public static DocentManager inst;

    GameObject myself;
    ViewTypeObserver observer;

    public List<Transform> cameraPositions = new List<Transform>();

    void Start()
    {
        if (inst == null)
            inst = this;
        else
            Destroy(this);
        Debug.Log("set inst");
    }

    public void CallPVResetCamera()
    {
        Debug.Log("reset camera PVC");
        if (myself == null)
            myself = GameObject.FindGameObjectWithTag("Myself");
        myself.GetComponent<CameraSetup>().ResetCamera();
    }

    public void CallPVChangeCamera(int CameraNumber)
    {
        Debug.Log("change camera PVC");
        if (myself == null)
            myself = GameObject.FindGameObjectWithTag("Myself");
        myself.GetComponent<CameraSetup>().ChangeCamera(CameraNumber);
    }

    public void CallPVChangeScene(int scene)
    {
        Debug.Log("change scene PVC");
        if (myself == null)
        {
            myself = GameObject.FindGameObjectWithTag("Myself");
        }

        myself.GetComponent<CameraSetup>().ChangeScene(scene);
    }

    public void CallPVOpenHamburger()
    {
        Debug.Log("open hamburger PVC");
        if (myself == null)
            myself = GameObject.FindGameObjectWithTag("Myself");
        myself.GetComponent<CameraSetup>().OpenHamburger();
    }

    public void ResetCamera()
    {
        Debug.Log("reset camera");
        myself.transform.SetParent(gameObject.transform);
    }

    public void ChangeCamera(int CameraNumber)
    {
        Debug.Log("change camera");
        myself.transform.SetParent(cameraPositions[CameraNumber]);
    }

    public void ChangeScene(int scene)
    {
        Debug.Log("change scene");
        if (observer == null) {
            observer = GameObject.Find("NetworkObservers/ViewTypeNetworkDevice").GetComponent<ViewTypeObserver>();
        }

        observer.changeScene(scene);
    }

    public void OpenHamburger()
    {
        Debug.Log("open hamburger");
        myself.gameObject.GetComponent<HamburgerMenu>().openMenu();
    }
}
