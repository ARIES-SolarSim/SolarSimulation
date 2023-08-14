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
                planets[i].transform.localPosition = new Vector3(0.5166248f, 0.04224792f, 0.4812764f);
            }
            else if (i == 2)
            {
                planets[i].transform.localPosition = new Vector3(-0.4128868f, -0.007746019f, -0.9266148f);
            }
            else if (i == 3)
            {
                planets[i].transform.localPosition = new Vector3(-1.121581f, 0.04553582f, 0.08777341f);
            }
            else if (i == 4)
            {
                planets[i].transform.localPosition = new Vector3(0.08973803f, -0.000822f, 1.822217f);

            }
            else if (i == 5)
            {
                planets[i].transform.localPosition = new Vector3(1.956572f, -0.03500602f, 1.624338f);

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
