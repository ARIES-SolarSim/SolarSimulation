using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempMoonScript : MonoBehaviour
{
    private float r = 2197.457f;
    private float lambda = 0f;
    //private float phi = 0f;

    public float lambdaRate = 0.04f;
    //private float phiRate = 0.001f;
    
    /*
     * This script is used as a place holder for a moon script for view 3. The update just moves the moon in a set circle around a center point.
     */
    void FixedUpdate()
    {
        transform.localPosition = new Vector3(r * Mathf.Cos(lambda * 2 * Mathf.PI / 180f), 0, r * Mathf.Sin(lambda * 2 * Mathf.PI / 180f));
        lambda = lambda + lambdaRate >= 360f ? lambda + lambdaRate - 360 : lambda + lambdaRate;
    }

    public float getLambda()
    {
        return lambda;
    }
}
