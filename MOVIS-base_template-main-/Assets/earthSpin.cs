using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class earthSpin : MonoBehaviour

    
{
    public float TiltAngle;
    public float SpinAngle;

    public float earthSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, earthSpeed, 0);
    }
}
