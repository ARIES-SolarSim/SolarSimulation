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

    public GameObject Mesh;
    public GameObject MeshAtmosphere;
    public GameObject RingObject;
    public PlanetController pc;

    private float[] Diameter = new float[] { 4.880739e-06f, 1.210831e-05f, 2.276055e-05f };

    //Elements
    public Material[] ElementsRocky;
    public Material[] ElementsGas;

    //Atmosphere
    public Color[] Atmospheres;

    //Planetary Rings - Currently all just Saturn Rings
    public Material[] Rings; //4

    private float[] DayLength = new float[] { 12f, 24f, 48f };

    //Mass                                      Merc        Venus      Earth      10x Mars
    private float[] MassOptions = new float[] { 1.3301176f, 4.171736f, 6.272128f, 6.6422288f };
    //                                                                 6.272128f
    //Initial Velocity
    private float[,] Velocity = new float[4, 3] 
    {
        { 0.048f, 0.055f, 0.09f },
        { 0.035f, 0.04f, 0.05f },
        { 0.025f, 0.032f, 0.038f }, //0.032
        { 0.021f, 0.027f, 0.028f }
    };
    //0.126470588
    private Vector3[] DistFromSun = {new Vector3(0.041176471f, 0f, 0f), new Vector3(0.082352941f, 0f, 0f), new Vector3(0.126470588f, 0f, 0f), new Vector3(0.185294118f, 0f, 0f)};

    private int[] Choices = new int[] { 1, 2, 1, 2, 2, 2, 3, 2 };
    //Surface Type (1-2), Size (1-3), Element (1-3), Atmosphere (1-3), Rings (1-4), Day Length (1-3), Distance From Sun (1-4), Velocity (1-3)

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

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 getDistFromSun()
    {
        return DistFromSun[Choices[DIST_FROM_SUN] - 1];
    }

    public float getVelocity()
    {
        //Debug.Log(DistFromSun[Choices[DIST_FROM_SUN] - 1]);
        return Velocity[Choices[DIST_FROM_SUN] - 1, Choices[VELOCITY] - 1];
    }

    public float getMass()
    {
        return MassOptions[Choices[DIST_FROM_SUN] - 1];
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
        MeshAtmosphere.GetComponent<MeshRenderer>().material.color = Atmospheres[Choices[ATMOSPHERE] - 1];

        RingObject.GetComponent<MeshRenderer>().material = Rings[Choices[RINGS] - 1];

        pc.rotationSpeed = DayLength[Choices[DAY_LENGTH] - 1];

        if(FindObjectOfType<UniverseController>().begin)
            pc.updateScale();

    }


    public void UpdateChoices(int choice)
    {
        Choices[choice/10] = choice%10;
    }

    public void UpdateSize()
    {
        Choices[SIZE] = (int)(Mathf.Round(sizeSliderUI.value));
        //Debug.Log(sizeSliderUI.value + " -> " + (int)(Mathf.Round(sizeSliderUI.value)));
    }
}
