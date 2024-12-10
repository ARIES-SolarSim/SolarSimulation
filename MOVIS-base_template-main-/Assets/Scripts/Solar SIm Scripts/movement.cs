using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{

    private Vector3 offset;
    GameObject[] bodies;
    GameObject earth;

    void Start()
    {
        bodies = GameObject.FindGameObjectsWithTag("bodies");
        foreach (GameObject b in bodies)
        {
            if(b.name == "proxyEarth")
            {
                earth = b;
            }
        }

        //PC CHANGE
        //offset = earth.transform.position - transform.position;
    }
    void Update()
    {
        //transform.position = earth.transform.position;
    }
}
