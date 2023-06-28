using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetBuilderSliderUpdater : MonoBehaviour
{
    private Slider slider;
    public int value; 
    // Start is called before the first frame update
    void Start()
    {
        slider = this.GetComponent<Slider>();
        value = (int)slider.value;
    }

    public void updateValue()
    {
        value = (int)slider.value;
    }
}
