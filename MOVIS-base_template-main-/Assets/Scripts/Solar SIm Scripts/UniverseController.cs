using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UniverseController : MonoBehaviour
{
    public static float bigG = 6.67428f * Mathf.Pow(10, -11); //G used for gravitation force calculating
    public static float planetScale = 1; //The scale planets are displayed
    public static float orbitScale = 1; //The scale of all orbits - can be used to scale the entire system at once rather than each individually
    public static int steps = 100; //How many steps the orbit of planets is calculated ahead of time. Affects the maximum speed.
    public static float timeStep = 0.0005f; //The frequency which the planets position is calculated
    public static int orbitSpeedK = 10; //The rate at which planets step through the points list

    public static bool orbiting = true; //Used to determine if planets are orbiting or changing view type
    public static int changeSteps = 0; //Used while changing view types
    public static int changeState = 0; //0 = Slowing down (default), 1 = changing orbit, 2 = speeding up, 3/0 = return to orbiting
    public static int currentSpeed = 0; //Current orbitSpeedK when changing
    public static float[] originalTime; //Will be used to store each planets time for trail renderer
    public static bool changed = false; //Not sure if actually needed but is here for now
    public static int count; //Created because of the way the for loops are done for now
    private PlanetController[] Planets; //List of planets to reference
    private VirtualController[] Bodies; //List of the virtual controlles in the planets to reference on build
    public RotateScript moon;
    public MeshScaler moonMesh;
    public bool isPlanetBuilder;
    public static int trailDelay = 5;
    public static int trailCount = 0;

    //The values below are placeholder for the motion profiling set up to smooth out the orbit scale transitions
    //Would be cool if this was done with cubic splines instead
    //public static float tolerance = 0.001f;
    public static int changeDuration = 800;
    public static int accDuration = 150;
    public PlanetController cameraLockedPlanet; //Which ever planet is currently locked to the camera view. This should replace a pined planet. WIP
    public AnimationCurve interpCurve;
    /*
     * Sets up all planets and virtual controllers
     */
    private void Awake()
    {
        if(isPlanetBuilder)
        {
            timeStep = 0.00005f;
            orbitScale = 16; //Scale to have first 4 planets to fill the space
            planetScale = 5;

        }
        Planets = FindObjectsOfType<PlanetController>(); //Fills the Planet list with all planets
        Bodies = new VirtualController[Planets.Length]; //Creates a list for all the virtual controllers
        for (int i = 0; i < Planets.Length; i++)
        {
            Bodies[i] = new VirtualController(Planets[i]); //Sets the virtual controller's planet to one of the planets from the list
        }
        InitiateVirtualControllers(); //
        for (int i = 0; i < Planets.Length; i++)
        {
            Planets[i].controller = Bodies[i];
            Planets[i].mesh.transform.localPosition = Vector3.zero; //Not sure if this is needed any longer
        }
        originalTime = new float[Planets.Length];
    }

    public void Start()
    {
        orbiting = true;
        if (ViewTypeObserver.immediateTransition)
        {
            changeDuration = 2;
            accDuration = 1;
            orbiting = false;
        }
    }
    public float curveInterp(float x)
    {
        //Debug.Log(interpCurve.Evaluate(x));
        return interpCurve.Evaluate(x);
    }

    /*
     * The Update method that handles moving the planets or changing viewtypes
     */
    void Update()
    {
        if(isPlanetBuilder)
        {
            if(trailCount < trailDelay)
            {
                trailCount++;
            }
        }
        if (!LobbyManager.userType) //Only orbit in headset, allow photon viewers to do the rest
        {
            if (orbiting)
            {
                move();
                currentSpeed = orbitSpeedK;
                moon.changing = false;

                if (ViewTypeObserver.immediateTransition == true)
                {
                    changeDuration = 800;
                    accDuration = 150;
                    ViewTypeObserver.immediateTransition = false;
                }
            }
            else //Changing viewtypes
            {
                moon.changing = true;
                if (changeState == 0) //Slowing down
                {
                    //Debug.Log("Decreasing Speed: " + accDuration + " " + changeSteps);
                    decreaseSpeed(accDuration, 0);
                }

                if (changeState == 1) //Changing
                {
                    //Debug.Log("Changing: " + changeDuration + " " + changeSteps);
                    moonMesh.changing();
                    //Debug.Log("Set isChanging to true but is: " + FindObjectOfType<MeshScaler>().isChanging);
                    foreach (PlanetController pc in Planets)
                    {
                        pc.diameter = pc.ViewTypeChangeMatrix[0][changeSteps];
                        pc.privateOrbitScale = pc.ViewTypeChangeMatrix[1][changeSteps];
                        pc.UpdateChangeValues();
                    }
                    if (changeSteps == changeDuration - 1)
                    {
                        changeState = 2;
                        changeSteps = 0;
                        MeshScaler.view = (MeshScaler.view == 1) ? 0 : 1;
                        FindObjectOfType<ViewTypeObserver>().transform.localPosition = new Vector3(0, 0, 0);

                        if (ViewTypeObserver.targetViewType == 1)
                        {
                            foreach (PlanetIdentifier pi in FindObjectsOfType<PlanetIdentifier>())
                            {
                                pi.showArrow();
                            }
                        }

                        else
                        {
                            foreach (PlanetIdentifier pi in FindObjectsOfType<PlanetIdentifier>())
                            {
                                pi.hideArrow();
                            }
                        }
                    }
                    changeSteps++;
                    //Debug.Log(changeSteps);
                }
                if (changeState == 2) //Speeding up
                {
                    moonMesh.doneChanging();
                    //Debug.Log("Speeding Back Up: " + accDuration + " " + changeSteps);
                    increaseSpeed(accDuration, 10);
                }
            }
        }
    }

    /*
     * Decreases the speed from the current value to the minimum value depending on the ratio of the provided time vs the changeStep
     */
    public void decreaseSpeed(float time, int min)
    {
        orbitSpeedK = Mathf.RoundToInt(((min - currentSpeed) / time * changeSteps) + currentSpeed);
        move();
        if (changeSteps == (int)time)
        {
            changeSteps = 0;
            changeState = 1;
            FindObjectOfType<ViewTypeObserver>().transform.localPosition = new Vector3(0, 0, 1);
            foreach (PlanetIdentifier pi in FindObjectsOfType<PlanetIdentifier>())
            {
                pi.hideArrow();
            }
        }
        changeSteps++;
    }

    /*
     * Increases the speed from the current value to the maximum value depending on the ratio of the provided time vs the changeStep
     */
    public void increaseSpeed(float time, int max)
    {
        orbitSpeedK = Mathf.RoundToInt(max * ((float)changeSteps) / time);
        move();
        if (changeSteps == (int)time)
        {
            changeSteps = 0;
            changeState = 0;
            orbiting = true;
        }
        changeSteps++;
    }

    /*
     * This method has each planet move through 1 iteration of points within the list.
     */
    public void move()
    {
        foreach (PlanetController pc in Planets)
        {
            pc.updateLocation();
        }
        updateVirtualControllers();
    }

    /*
     * This method fills the point list with all of the future points that the planet will follow up to a set limit. Once the system is running, the points
     * list will be periodically updated to show the next steps required. The maximum amount of points calculated is also the maximum speed the planets can
     * travel.
     *
     * To use only if each point's list needs to be completely remade. A change in orbit speed or scale does not need a new points list
     */
    public void InitiateVirtualControllers()
    {
        foreach (VirtualController vc in Bodies) //If used while points lists are full, clears them
        {
            vc.points.Clear();
        }
        for (int i = 0; i < steps; i++)
        {
            for (int j = 0; j < Bodies.Length; j++) //Sets new Velocities
            {
                Bodies[j].velocity = Bodies[j].CalculateVelocity(Bodies, timeStep);
            }
            for (int j = 0; j < Bodies.Length; j++)
            {
                Vector3 newPos = Bodies[j].position + Bodies[j].velocity * timeStep;
                Bodies[j].position = newPos;
                Bodies[j].points.AddLast(newPos); //Points list do not change when orbit scale is changed. That should occur within PlanetController
            }
        }
    }
    /*
     * This method removes the points that the planet has traveled through and refreshes the other side of the list with an equal amount of new points.
     *
     * Used to find the next orbitSpeedK spaces in points
     */
    public void updateVirtualControllers()
    {
        foreach (VirtualController vc in Bodies) //Removes the index that planets should currently be at
        {
            for (int i = 0; i < orbitSpeedK; i++)
            {
                vc.points.RemoveFirst();
            }
        }

        for (int i = 0; i < orbitSpeedK; i++) //Similar to initiateVirtualController, this calculates the next orbitSpeedK number of points
        {
            for (int j = 0; j < Bodies.Length; j++)
            {
                Bodies[j].velocity = Bodies[j].CalculateVelocity(Bodies, timeStep);
            }
            for (int j = 0; j < Bodies.Length; j++)
            {
                Vector3 newPos = Bodies[j].position + Bodies[j].velocity * timeStep;
                Bodies[j].position = newPos;
                Bodies[j].points.AddLast(newPos);
            }
        }
    }

    /**
     * Added by Shane to help slider functionality 
     * This might be entirely the wrong thing to do, in which case, execute me
     */
    public void OrbitSpeedKChange(int i)
    {
        orbitSpeedK = i; 
    }
}