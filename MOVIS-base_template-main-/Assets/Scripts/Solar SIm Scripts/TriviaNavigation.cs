using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriviaNavigation : MonoBehaviour
{

    private GameObject[] planets;
    // Start is called before the first frame update
    void Start()
    {
        planets = GameObject.FindGameObjectsWithTag("planet");
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Scatter()
    {
        Debug.Log("IN SCATTER");
        for (int i = 0; i < planets.Length; i++)
        {
            if (i == 0)
            {
                planets[i].transform.localPosition = new Vector3(0, 0, 0);

            }
            else if(i == 1)
            {
            }
            else if (i == 2)
            {

            }
            else if (i == 3)
            {

            }
            else if (i == 4)
            {

            }
            else if (i == 5)
            {

            }
            else if (i == 6)
            {

            }
            else if (i == 7)
            {

            }
            else
            {

            }
        }
    }
}
