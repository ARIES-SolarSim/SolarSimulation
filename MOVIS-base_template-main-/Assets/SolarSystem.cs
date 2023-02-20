using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystem : MonoBehaviour
{

    readonly float G = 100f;
    GameObject[] bodies;
    GameObject cameraLockedPlanet;
    readonly Vector3 m_EulerAngleVelocity = new Vector3(0, 1, 0);

    // Start is called before the first frame update
    void Start()
    {
        bodies = GameObject.FindGameObjectsWithTag("bodies");
        cameraLockedPlanet = bodies[1];

        InitialVelocity();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Gravity();
        
    }

    void Gravity()
    {
  
        foreach (GameObject a in bodies)
        {
            foreach (GameObject b in bodies)
            {
                if (!a.Equals(b))
                {
                    float m1 = a.GetComponent<Rigidbody>().mass;
                    float m2 = b.GetComponent<Rigidbody>().mass;

                    float r = Vector3.Distance(a.transform.position, b.transform.position);

                    a.GetComponent<Rigidbody>().AddForce((b.transform.position - a.transform.position).normalized * (G * (m1 * m2) / (r * r)));
                    

                }
            }
           
        }
    }

    void InitialVelocity()
    {
        foreach (GameObject a in bodies)
        {
            foreach (GameObject b in bodies)
            {
                if (!a.Equals(b))
                {
                    float m2 = b.GetComponent<Rigidbody>().mass;

                    float r = Vector3.Distance(a.transform.position, b.transform.position);

                    a.transform.LookAt(b.transform);

                    a.GetComponent<Rigidbody>().velocity += a.transform.right * Mathf.Sqrt((G * m2) / r);

                }
            }
        }
    }
}
