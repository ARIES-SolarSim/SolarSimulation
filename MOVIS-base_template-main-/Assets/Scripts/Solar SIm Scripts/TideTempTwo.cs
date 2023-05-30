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
        float subtraction_factor = (float)Math.Atan(myZ / myX);
        float newNum;

        /*if(original_number >= 0 && original_number2 >= 0) // Pos Pos
        {
            newNum = (float)Math.Atan(original_number2 / (original_number));
            Debug.Log("Pos Pos - " + newNum);
        }
        else if (original_number >= 0 && original_number2 < 0) // Pos Neg
        {
            newNum = (float)Math.PI - (float)Math.Abs((float)Math.Atan(original_number2 / (original_number)));
            Debug.Log("Pos Neg - " + newNum);
        }
        else if (original_number < 0 && original_number2 >= 0) // Neg Pos
        {
            newNum = (float)Math.PI +(float)Math.Atan(original_number2 / (original_number));
            Debug.Log("Neg Pos - " + newNum);
        }
        else //Neg Neg
        {
            newNum = (float)(2*Math.PI) + (float)Math.Atan(original_number2 / (original_number));
            Debug.Log("Neg Neg - " + newNum);
        }*/
        newNum = Mathf.Atan2(original_number2, original_number) + Mathf.PI;
        Debug.Log(newNum);

        float newY = (float)(2*Math.Cos(0.5*Math.PI * (newNum+subtraction_factor) + Math.PI) + 5);
        transform.localPosition = new Vector3(5, newY, 4.67f); 
        }
    //x: -4.73547, z:4.226318

}
