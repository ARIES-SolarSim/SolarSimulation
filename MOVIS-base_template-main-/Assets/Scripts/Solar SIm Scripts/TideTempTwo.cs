using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TideTempTwo : MonoBehaviour
{
    //private RotateAround Moon;
    public float minY;
    public float maxY;
    public float myX;
    public float myZ;
    private void Start()
    {
        //Moon = FindObjectOfType<RotateAround>();
    }

    // Update is called once per frame
    void Update()
    {
        float original_number = GameObject.Find("proxyMoon").transform.position.x;
        float original_number2 = GameObject.Find("proxyMoon").transform.position.z;
        float newY = (Math.Abs(original_number) / 6.7f) * 4 + 3;
        transform.localPosition = new Vector3(5, newY, 4.67f);
        float newNum = (float)Math.Atan(original_number2 / original_number);
        Debug.Log(2 * Math.Cos(2 * Math.PI * newNum + Math.PI) + 5);
    }
}
