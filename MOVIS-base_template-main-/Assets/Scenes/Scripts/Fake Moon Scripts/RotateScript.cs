using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScript : MonoBehaviour
{
    public Transform target;
    public PlanetController targetController;

    public bool changing = false;
    public bool correct = false;

    public int view = 1;

    private Vector3[] scales = {new Vector3(0.000003476238745f, 0.000003476238745f, 0.000003476238745f),
        new Vector3(0.05079997257f, 0.05079997257f, 0.05079997257f), new Vector3(0.228599877f, 0.228599877f, 0.228599877f)};
    private Vector3[] positions = {new Vector3(0.001f, -0.006858f, 0f), new Vector3(0.149878f, -0.006858f, 0f), new Vector3(0.149878f, -0.006858f, 0f) };

    public GameObject theMesh;
    public GameObject arrow;

    // Start is called before the first frame update
    void Start()
    {
        //gameObject.GetComponent<TrailRenderer>().enabled = false;
        transform.position = new Vector3(0.001f, -0.006858f, 0f) + target.position;
        //gameObject.GetComponent<TrailRenderer>().Clear();
        //gameObject.GetComponent<TrailRenderer>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!changing)
        {
            if (!correct)
            {
                correct = true;
                //TrailRenderer trail = gameObject.GetComponent<TrailRenderer>();
                //trail.Clear();
                //trail.enabled = false;
                transform.position = positions[view - 1] + target.transform.position; //target.transform.position + 
                /* if (!UniverseController.orbiting)
                {
                    gameObject.GetComponent<TrailRenderer>().enabled = false;
                }
                */
                if (view == 1)
                {
                    //gameObject.GetComponent<TrailRenderer>().enabled = true;
                    arrow.SetActive(true);
                }
                else //view 2
                {
                    //gameObject.GetComponent<TrailRenderer>().enabled = true;
                    arrow.SetActive(false);
                }

                //trail.enabled = true;
            }

            if (view == 1)
            {
                transform.RotateAround(target.position, Vector3.up, 900f * UniverseController.orbitSpeedK * UniverseController.timeStep);
            }
            else
            {
                transform.RotateAround(target.position, Vector3.up, 15 * 6 * UniverseController.orbitSpeedK * UniverseController.timeStep);
                //transform.RotateAround(target.position, Vector3.up, 6 * UniverseController.orbitSpeedK * Time.deltaTime);
            }
        }

        else
        {
            //gameObject.GetComponent<TrailRenderer>().enabled = false;
            correct = false;
        }
    }
}
