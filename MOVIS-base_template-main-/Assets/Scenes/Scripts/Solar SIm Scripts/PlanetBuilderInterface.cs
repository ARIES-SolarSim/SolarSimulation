using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public GameObject Mesh;
    public GameObject MeshAtmosphere;
    public PlanetController pc;

    public Material Rocky;
    public Material Gas;

    private float[] Diameter = new float[] { 4.880739e-06f, 1.210831e-05f, 1.276055e-05f };

    //Elements
    public Color[] Elements;

    //Atmosphere
    public Color[] Atmospheres;

    //Planetary Rings
    public GameObject[] Rings = new GameObject[3];

    private float[] DayLength = new float[] { 12f, 24f, 48f };

    //Mass                                      Merc        Venus      Earth      10x Mars
    private float[] MassOptions = new float[] { 1.3301176f, 4.171736f, 6.272128f, 6.6422288f };

    //Initial Velocity
    private float[,] Velocity = new float[4, 3] 
    {
        { 0.048f, 0.055f, 0.09f },
        { 0.035f, 0.04f, 0.05f },
        { 0.025f, 0.032f, 0.038f },
        { 0.021f, 0.027f, 0.028f }
    };

    private Vector3[] DistFromSun = {new Vector3(0.041176471f, 0f, 0f), new Vector3(0.082352941f, 0f, 0f), new Vector3(0.126470588f, 0f, 0f), new Vector3(0.185294118f, 0f, 0f)};

    private int[] Choices = new int[] { 1, 2, 1, 2, 2, 2, 4, 3 };
    //Surface Type (1-2), Size (1-3), Element (1-3), Atmosphere (1-3), Rings (1-3), Day Length (1-3), Distance From Sun (1-4), Mass (1-3)

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
        Debug.Log(DistFromSun[Choices[DIST_FROM_SUN] - 1]);
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
        if (Choices[SURFACE_TYPE] == 1)
        {
            Mesh.GetComponent<MeshRenderer>().material = Rocky;
        }
        else
        {
            Mesh.GetComponent<MeshRenderer>().material = Gas;
        }

        pc.diameter = Diameter[Choices[SIZE] - 1];

        Mesh.GetComponent<MeshRenderer>().material.color = Elements[Choices[ELEMENT] - 1];

        MeshAtmosphere.GetComponent<MeshRenderer>().material.color = Atmospheres[Choices[ELEMENT] - 1];

        //Set Rings

        pc.rotationSpeed = DayLength[Choices[DAY_LENGTH] - 1];

        //Set Dist from Sun

        pc.updateScale();

    }
}
