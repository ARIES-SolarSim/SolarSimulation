using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempMoonScript : MonoBehaviour
{
    private float r = 2197.457f;
    private float lambda = 0f;
    private float phi = 0f;

    private float lambdaRate = 0.01f;
    private float phiRate = 0.001f;

    void Update()
    {
        transform.localPosition = new Vector3(r * Mathf.Cos(lambda * 2 * Mathf.PI / 180f), 0, r * Mathf.Sin(lambda * 2 * Mathf.PI / 180f));
        lambda = lambda + lambdaRate >= 360f ? lambda + lambdaRate - 360 : lambda + lambdaRate;
    }
}
