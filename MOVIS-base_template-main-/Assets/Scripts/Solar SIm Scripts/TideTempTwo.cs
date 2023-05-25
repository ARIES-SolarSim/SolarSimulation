using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TideTempTwo : MonoBehaviour
{
    //private RotateAround Moon;
    public float minY;
    public float maxY;

    private void Start()
    {
        //Moon = FindObjectOfType<RotateAround>();
    }

    // Update is called once per frame
    void Update()
    {
        float original_number = GameObject.Find("proxyMoon").transform.position.x;
        float newY = (Math.Abs(original_number) / 6.8f) * 4 + 3;
        transform.localPosition = new Vector3(5, newY, 4.67f);
    }
}
