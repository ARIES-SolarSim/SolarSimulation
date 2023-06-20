using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdjustHeight : MonoBehaviour
{
    public GameObject universe;
    public Slider slider;

    public float previousValue;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = 1f;
        previousValue = slider.value;
    }

    // Update is called once per frame
    void Update()
    {
        universe.transform.position = new Vector3(0, slider.value, 0);
        if(slider.value != previousValue)
        {
            PlanetController[] PlanetList = FindObjectsOfType<PlanetController>();
            TrailRenderer[] trails = new TrailRenderer[PlanetList.Length];
            for(int i = 0; i < PlanetList.Length; i ++)
            {
                trails[i] = PlanetList[i].GetComponent<TrailRenderer>();
            }
            //Update height
        }
        previousValue = slider.value;
    }
}
