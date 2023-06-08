using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedController : MonoBehaviour
{
    public UniverseController uC;
   
    private Slider slider;
    private int potentialMax = UniverseController.steps;
    private void Start()
    { 
        slider = this.gameObject.GetComponent<Slider>();
        if (slider.maxValue > potentialMax) // Make sure it doesn't exceed the set Max
        {
            slider.maxValue = potentialMax;
        }
    }
    public void ChangeSpeed()
    { 
        uC.OrbitSpeedKChange((int)this.gameObject.GetComponent<Slider>().value);
    }
}
