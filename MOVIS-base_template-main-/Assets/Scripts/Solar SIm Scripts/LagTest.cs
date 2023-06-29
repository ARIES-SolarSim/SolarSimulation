using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LagTest : MonoBehaviour
{
    private float curTime = 0f;

    void Update()
    {
        curTime += Time.deltaTime;
        float y = 0.5f *  Mathf.Sin(curTime);
        //Debug.Log(y);
        transform.localPosition = new Vector3(transform.localPosition.x, 1 + y, transform.localPosition.z);
    }
}
