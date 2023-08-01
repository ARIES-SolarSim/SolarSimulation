using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tideHolder : MonoBehaviour
{

    private Vector3 camera;
    private Vector3 distance;

    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponentInParent<Transform>().position;
        distance = camera - gameObject.GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = camera - distance;
    }
}
