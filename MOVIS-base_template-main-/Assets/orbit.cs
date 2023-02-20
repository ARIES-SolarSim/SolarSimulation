using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orbit : MonoBehaviour
{
    readonly float G = 100f;
    GameObject[] bodies;
    GameObject earth;
    // Start is called before the first frame update
    void Start()
    {
        bodies = GameObject.FindGameObjectsWithTag("bodies");
        earth = bodies[1];
    }

    // Update is called once per frame
    void Update()
    {
        float u = G * (GetComponent<Rigidbody>().mass + earth.GetComponent<Rigidbody>().mass);
        
        float e = (u / (2 * .0384400f));
        Debug.Log(e);
        float h = transform.position.y * earth.transform.position.y;
        float y = Mathf.Sqrt(((2 * e * h * h) / (u * u)));
        Debug.Log(y);
        transform.position = new Vector3(transform.position.x, y, transform.position.z);

    }
}
