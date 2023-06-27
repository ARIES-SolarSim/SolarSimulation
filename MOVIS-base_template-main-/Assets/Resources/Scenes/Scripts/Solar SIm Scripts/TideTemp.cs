using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TideTemp : MonoBehaviour
{
    private RotateAround Moon;
    public float minZ;
    public float maxZ;

    private void Start()
    {
        Moon = FindObjectOfType<RotateAround>();
    }

    // Update is called once per frame
    void Update()
    {
        float newZ = ((Mathf.Cos(Moon.getLambda() * 2 * Mathf.PI / 180f) / 2f) + 0.5f) * (maxZ - minZ) + minZ;
        transform.localPosition = new Vector3(0, 0, newZ);
    }
}
