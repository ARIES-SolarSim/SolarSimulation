using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Photon.Pun;

public class TideTempTwo : MonoBehaviour
{
    //private RotateAround Moon;
    public float minY;
    public float maxY;
    public float myX;
    public float myZ;
    public Camera cam;
    private MeshFilter m;
    private Mesh p;
    private Vector3[] VertexList;
    public GameObject drop;
    //public float sunminY;
    //public float sunmaxY;
    public GameObject proxyMoon;
    //public GameObject proxySun;

    private void Start()
    {
        
        //Moon = FindObjectOfType<RotateAround>();
        proxyMoon = GameObject.Find("Arrow");
        //proxySun = GameObject.Find("proxySun");
        //x: -5.25763, z: 3.989652

        if (PhotonNetwork.NickName == "9" || !LobbyManager.userType)
        {
            Debug.Log("DROPDOWN HERE");
            drop.SetActive(true);
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (!LobbyManager.userType)
        {
            float original_number = proxyMoon.transform.position.x;
            float original_number2 = proxyMoon.transform.position.z;
            //float sun_number = proxySun.transform.position.x;
            //float sun_number2 = proxySun.transform.position.z;
            float subtraction_factor = Mathf.PI - Mathf.Atan(myZ / myX);
            float newNum, newNum2;

            newNum = Mathf.Atan2(original_number2, original_number);
            //newNum2 = Mathf.Atan2(sun_number2, sun_number);
            float newY = ((Mathf.Cos(2 * newNum + subtraction_factor) + 1) / 2.0f) * (maxY - minY) + minY;
            //float newY2 = ((Mathf.Cos(12 * newNum2 + subtraction_factor) + 1) / 2.0f) * (sunmaxY - sunminY) + sunminY;
            transform.localPosition = new Vector3(5, newY, 4.67f);
            
        }

    }

    
}
