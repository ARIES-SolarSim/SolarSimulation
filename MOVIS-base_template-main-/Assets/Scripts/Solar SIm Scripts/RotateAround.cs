using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public GameObject targetCenter;
    public float speed;

    private float lambda = 0f;
    private float r = 0f;

    private void Start()
    {
        r = transform.localPosition.z;
    }


    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(r * Mathf.Cos(lambda * 2 * Mathf.PI / 180f), 0, r * Mathf.Sin(lambda * 2 * Mathf.PI / 180f));
        lambda = lambda + speed >= 360f ? lambda + speed - 360 : lambda + speed;
    }

    public float getLambda()
    {
        return lambda;
    }
}
