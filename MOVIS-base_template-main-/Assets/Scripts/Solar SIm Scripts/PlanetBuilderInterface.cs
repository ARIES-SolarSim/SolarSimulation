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
    public PlanetController pc;

    public Material Rocky;
    public Material Gas;

    private float[] Diameter = new float[] { 0.1f, 0.2f, 0.3f };

    //Elements

    //Atmosphere

    //Planetary Rings
    public GameObject[] Rings = new GameObject[3];

    private float[] DayLength = new float[] { 12f, 24f, 48f };

    //Mass
    private float[,] MassOptions = new float[4, 3]
    {
        { 1.0f, 2.0f, 3.0f },
        { 4.0f, 5.0f, 6.0f },
        { 7.0f, 8.0f, 9.0f },
        { 10.0f, 11.0f, 12.0f }
    };

    //Initial Velocity
    private float[] Velcoity = new float[] { 1.0f, 2.0f, 3.0f, 4.0f };

    private float[] DistFromSun = new float[] { 1.0f, 2.0f, 3.0f };

    private int[] Choices = new int[] { 1, 2, 2, 2, 1, 2, 3, 2 };
    //Surface Type (1-2), Size (1-3), Element (1-3), Atmosphere (1-3), Rings (1-3), Day Length (1-3), Distance From Sun (1-4), Mass (1-3)

    public readonly int SURFACE_TYPE = 0;
    public readonly int SIZE = 1;
    public readonly int ELEMENT = 2;
    public readonly int ATMOSPHERE = 3;
    public readonly int RINGS = 4;
    public readonly int DAY_LENGTH = 5;
    public readonly int DIST_FROM_SUN = 6;
    public readonly int MASS = 7;

    void Start()
    {
        updateVisuals();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateVisuals()
    {
        if (Choices[SURFACE_TYPE] == 1)
        {
            //Set material to rocky
        }
        else
        {
            //Set material to Gas
        }

        pc.diameter = Diameter[Choices[SIZE] - 1];

        if (Choices[ELEMENT] == 1)
        {
            //Set element material to 1
        }
        else if (Choices[ELEMENT] == 2)
        {
            //Set element material to 2
        }
        else
        {
            //Set element material to 3
        }

        if (Choices[ATMOSPHERE] == 1)
        {
            //Set atmosphere material to 1
        }
        else if(Choices[ATMOSPHERE] == 2)
        {
            //Set atmosphere material to 2
        }
        else
        {
            //Set atmosphere material to 3
        }

        //Set Rings

        pc.rotationSpeed = DayLength[Choices[DAY_LENGTH] - 1];

        //Set Dist from Sun

        pc.mass = MassOptions[Choices[DIST_FROM_SUN] - 1, Choices[MASS] - 1];
        pc.updateScale();

    }
}
