using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystem : MonoBehaviour
{
    //Make sure that proxyEarth under the tag bodies has mesh checked on in order to provide shadows for moon
    //Make sure all the objects under the tag proxy have mesh unchecked
    readonly float G = 100f;
    GameObject[] bodies;
    GameObject[] proxy;
    GameObject proxyEarth;
    GameObject lil;
    GameObject earth;
    //readonly Vector3 m_EulerAngleVelocity = new Vector3(0, 1, 0);

    // Start is called before the first frame update
    void Start()
    {
        //These are the planets that we see moving around earth
        bodies = GameObject.FindGameObjectsWithTag("bodies");

        //These are the planets moving along the correct path circling the sun
        proxy = GameObject.FindGameObjectsWithTag("proxyBodies");
     
        //This is the earth for the proxy values
        proxyEarth = proxy[0];

        //This is the earth for the body values
        earth = bodies[1];

        //Tis is the little earth inside the big earth
        lil = GameObject.FindGameObjectsWithTag("lil")[0];

        InitialVelocity();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Gravity();
        
    }

    void Gravity()
    {
  
        //This function provides the gravity using solar system physics
        foreach (GameObject a in proxy)
        {
            foreach (GameObject b in proxy)
            {
                if (!a.Equals(b))
                {
                    float m1 = a.GetComponent<Rigidbody>().mass;
                    float m2 = b.GetComponent<Rigidbody>().mass;

                    float r = Vector3.Distance(a.transform.position, b.transform.position);
                    
                    a.GetComponent<Rigidbody>().AddForce((b.transform.position - a.transform.position).normalized * (G * (m1 * m2) / (r * r)));

                   
                    

                }
            }
            //This loop makes it so the planets we see are circling earth with earth staying still
            foreach (GameObject c in bodies)
            {
                if (c.name == a.name)
                {
                    c.transform.position = a.transform.position - proxyEarth.transform.position;
                }
            }

            //This makes little earth positioned where it should be inside of earth
            lil.transform.position = earth.transform.position;

        }
    }

    void InitialVelocity()
    {
        //This function provides the initial velocity using solar system physics

        foreach (GameObject a in proxy)
        {
            foreach (GameObject b in proxy)
            {
                if (!a.Equals(b) )  
                {
                    float m2 = b.GetComponent<Rigidbody>().mass;

                    float r = Vector3.Distance(a.transform.position, b.transform.position);

                    a.transform.LookAt(b.transform);

                    a.GetComponent<Rigidbody>().velocity += a.transform.right * Mathf.Sqrt((G * m2) / r);

                }
               


            }
            
            //This loop makes it so the planets we see are circling earth with earth staying still
            foreach (GameObject c in bodies)
            {
                if (c.name == a.name)
                {
                    c.transform.position = a.transform.position - proxyEarth.transform.position;
                }
            }

            //This makes little earth positioned where it should be inside of earth
            lil.transform.position = earth.transform.position;
        }
    }
}
