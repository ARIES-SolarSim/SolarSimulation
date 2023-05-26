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
        float newNum;
        if (original_number != 0)
        {
            newNum = (float)Math.Atan(original_number2 / original_number);
        }
        else if (original_number2 > 0)
        {
            newNum = (float)Math.PI / 2;
            Debug.Log(2 * Math.Cos(Math.PI * newNum + Math.PI) + 5);
        }
        else
        {
            newNum = (float)((3*Math.PI) / 2);
            Debug.Log(2 * Math.Cos(Math.PI * newNum + Math.PI) + 5);
        }
        float newY = (float)(2*Math.Cos(0.5*Math.PI * newNum + Math.PI) + 5);
        transform.localPosition = new Vector3(5, newY, 4.67f); 
        }

    }
