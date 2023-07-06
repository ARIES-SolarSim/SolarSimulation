using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.localEulerAngles = new Vector3(15, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0.1f, 0);
    }
}
