using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformSolarSystem : MonoBehaviour
{
    public Transform earth;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(earth.position, Vector3.down, speed * Time.deltaTime);
    }
}
