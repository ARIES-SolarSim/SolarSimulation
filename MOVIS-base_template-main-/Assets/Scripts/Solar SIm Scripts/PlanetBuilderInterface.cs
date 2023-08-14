using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetBuilderInterface : MonoBehaviour
{
    /*
     * Rocky Vs Gas Giant
     * Size (3 Choices)
     * Elements / Color (TBD Choices)
     * Atmosphere Material (TBD Choices)
     * Planetary Rings (None, Small, Big)
     * Length of Day (TBD Choices)
     * Distance From Sun (4 Choices)
     * Mass (3 Choices)
     */

    public Slider sizeSliderUI;
    public Slider atmosphereUI;


    public GameObject Mesh;
    public GameObject MeshAtmosphere;
    public GameObject RingObject;
    public PlanetController pc;

    private float[] Diameter = new float[] { 6.880739e-06f, 1.210831e-05f, 1.876055e-05f };

    //Elements
    public Material[] ElementsRocky;
    public Material[] ElementsGas;

    //Atmosphere
    public Color[] Atmospheres;

    //Planetary Rings - Currently all just Saturn Rings
    public Material[] Rings; //4
    
    private float[] DayLength = new float[] { 1.946f, 0.973f, 0.4865f };
    
    //Need to do next 4 Masses
    //Mass                                      Merc        Venus      Earth      10x Mars  | Merc        Venus      Earth      10x Mars 
    private float[] MassOptions = new float[] { 1.3301176f, 4.171736f, 6.272128f, 6.6422288f, 1.3301176f, 4.171736f, 6.272128f, 6.6422288f };
    
    //Need to do next 4 Velocities
    //Initial Velocity
    private float[,] Velocity = new float[8, 3] 
    {
        { 0.048f, 0.055f, 0.090f },
        { 0.035f, 0.040f, 0.050f },
        { 0.025f, 0.032f, 0.038f },
        { 0.021f, 0.027f, 0.028f },

        { 0.048f, 0.055f, 0.090f },
        { 0.035f, 0.040f, 0.050f },
        { 0.025f, 0.032f, 0.038f },
        { 0.021f, 0.027f, 0.028f }
    };

    //Need to do next 4 Distances
    private Vector3[] DistFromSun = {new Vector3(0.041176471f, 0f, 0f),
                                     new Vector3(0.082352941f, 0f, 0f), 
                                     new Vector3(0.126470588f, 0f, 0f), 
                                     new Vector3(0.185294118f, 0f, 0f),

                                     new Vector3(0.041176471f, 0f, 0f),
                                     new Vector3(0.082352941f, 0f, 0f),
                                     new Vector3(0.126470588f, 0f, 0f),
                                     new Vector3(0.185294118f, 0f, 0f)};

    private int[] Choices = new int[] { 1, 2, 1, 2, 2, 2, 3, 2 };
    //Surface Type (1-2), Size (1-3), Element (1-6), Atmosphere (1-3), Rings (1-4), Day Length (1-3), Distance From Sun (1-4), Velocity (1-3)

    public readonly int SURFACE_TYPE = 0;
    public readonly int SIZE = 1;
    public readonly int ELEMENT = 2;
    public readonly int ATMOSPHERE = 3;
    public readonly int RINGS = 4;
    public readonly int DAY_LENGTH = 5;
    public readonly int DIST_FROM_SUN = 6;
    public readonly int VELOCITY = 7;


    void Start()
    {
        updateVisuals();
    }

    public Vector3 getDistFromSun()
    {
        if (Choices[SURFACE_TYPE] == 1) //Rocky
        {
            return DistFromSun[Choices[DIST_FROM_SUN] - 1];
        }
        else //Gas
        {
            return DistFromSun[Choices[DIST_FROM_SUN] - 1 + 4];
        }
    }

    public float getVelocity()
    {
        if (Choices[SURFACE_TYPE] == 1) //Rocky
        {
            return Velocity[Choices[DIST_FROM_SUN] - 1, Choices[VELOCITY] - 1];
        }
        else
        {
            return Velocity[Choices[DIST_FROM_SUN] - 1 + 4, Choices[VELOCITY] - 1];
        }
    }

    public float getMass()
    {
        if (Choices[SURFACE_TYPE] == 1) //Rocky
        {
            return MassOptions[Choices[DIST_FROM_SUN] - 1];
        }
        else
        {
            return MassOptions[Choices[DIST_FROM_SUN] - 1 + 4];
        }
    }

    public void updateVisuals()
    {
        if (Choices[SURFACE_TYPE] == 1) //Rocky
        {
            Mesh.GetComponent<MeshRenderer>().material = ElementsRocky[Choices[ELEMENT] - 1];
        }
        else
        {
            Mesh.GetComponent<MeshRenderer>().material = ElementsGas[Choices[ELEMENT] - 1];
        }

        pc.diameter = Diameter[Choices[SIZE] - 1];
        pc.updateScale();

        Color temp = Atmospheres[Choices[ATMOSPHERE] - 1];
        temp.a = (float)(Mathf.Round(atmosphereUI.value)) / 255;
        MeshAtmosphere.GetComponent<MeshRenderer>().material.color = temp;

        RingObject.GetComponent<MeshRenderer>().material = Rings[Choices[RINGS] - 1];

        pc.rotationSpeed = DayLength[Choices[DAY_LENGTH] - 1];

        if(FindObjectOfType<UniverseController>().begin)
            pc.updateScale();

    }


    public void UpdateChoices(int choice)
    {
        Choices[choice/10] = choice%10;
        if(choice/10 == ELEMENT)
        {
            Debug.Log(choice / 10);
        }
    }

    public void UpdateSize()
    { //Temporary, limits the size range. Should be chanegd to a slider-like value range.
        int val = (int)(Mathf.Round(sizeSliderUI.value));
        Choices[SIZE] = (val > 3) ? 3 : val;
        Debug.Log(sizeSliderUI.value);
    }
}
