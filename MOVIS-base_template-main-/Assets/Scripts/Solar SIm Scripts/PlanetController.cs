using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlanetController : MonoBehaviour
{
    public float diameter; //The demonstrated diameter of the planet. This value does not alter the forces of the planet.
    public float mass; //The simulate mass of the planet. This value should NOT change.
    public Vector3 InitialPosition; //The starting position for the planet
    public Vector3 InitialVelocity; //The starting velocity for the planet
    public float tiltAngle; //The angle at which the planet is tilted
    public float rotationSpeed; //The speed at which the planet rotates around its axis
    public float privateOrbitScale; //The individual scale of this planets orbit. Used for view 2 only, set to 1 for view 1
    public GameObject mesh; //The reference to the mesh of the planet. Set in the prefab
    public int ID; //The unique ID of the planet. (Sun = 0, Mercury = 1, Venus = 2...)
    public Vector3 MathPosition { get; set; } //The true position of the planet, not altered by scaling or shifting
    public Vector3 Velocity { get; set; } //The current velocity of the planet, used for calculations of future points
    public VirtualController controller { get; set; } //The virtual controller assigned to this planet
    /*
     * A matrix used to smooth out the transistion between view 1 and view 2.
     * The first row is the diameter and the second is the orbit scale. Each row
     * is UniverseController.changeDuration and contains a smooth transition
     * between the current and the target values
     */
    public float[][] ViewTypeChangeMatrix { get; set; }

    public ViewTypeObserver trailObserver;
    public float trailTime;
    public TrailRenderer tr;

    /*
     * The awake initiates several values referencing from the virtualController.
     *
     * MathPos should be used for any velocity, position, or acceleration calculations and is not affected by scale
     * Rigidbody's position values should change due to scale and are not used in any calulations.
     */
    void Awake()
    {
        Velocity = InitialVelocity;
        mesh.transform.localScale = Vector3.one * diameter; //Could move to editmode script if needed
        transform.localPosition = InitialPosition;
        MathPosition = transform.localPosition * privateOrbitScale;
        mesh.transform.eulerAngles += new Vector3((ID == 3) ? tiltAngle : 0, 0, (ID != 3) ? tiltAngle : 0);
        if (ID == 4) //Disables the current moon
        {
            this.gameObject.SetActive(false);
        }
    }

    /*
     * Updates the current location of the planet controller to the next value in the list.
     */
    public void updateLocation()
    {
        if (!LobbyManager.userType)
        {
            MathPosition = controller.points.First.Value;
            transform.localPosition = (MathPosition - GetComponentInParent<UniverseController>().cameraLockedPlanet.controller.points.First.Value) * UniverseController.orbitScale * privateOrbitScale;
        }
    }

    public void Update()
    {
        if(ID != 0 && ID != 4)
        {
            if (trailObserver.transform.localPosition.z == 1)
            {
                tr.time = 0;
            }
            else
            {
                tr.time = trailTime;
            }
        }
    }

    /*
     * Fills in the change matrix with the current sloping values
     */
    public void changeViewType(int ViewType)
    {
        UniverseController uc = FindObjectOfType<UniverseController>();
        PlanetData pd = GetComponentInParent<PlanetData>();
        float[][] changeMatrix = new float[2][];
        for (int i = 0; i < changeMatrix.Length; i++) //Sets up the changematrices
        {
            changeMatrix[i] = new float[UniverseController.changeDuration];
        }
        if(ID == 5)
        {
            //Debug.Log(pd.PlanetList[ID].Diameter[0] + " " + pd.PlanetList[ID].Diameter[1] + " " + ViewType);
        }
        for (int i = 0; i < UniverseController.changeDuration; i++)
        {
            float index = ((float)i) / UniverseController.changeDuration;
            changeMatrix[0][i] = uc.curveInterp(index) * (pd.PlanetList[ID].Diameter[ViewType - 1] - diameter) + diameter;
            changeMatrix[1][i] = uc.curveInterp(index) * (pd.PlanetList[ID].OrbitScale[ViewType - 1] - privateOrbitScale) + privateOrbitScale;
            if (ID == 5)
            {
                //Debug.Log("I: " + i + " Curve: " + uc.curveInterp(index) + " Matrix: " + changeMatrix[0][i]);
            }
        }
        changeMatrix[0][UniverseController.changeDuration - 1] = pd.PlanetList[ID].Diameter[ViewType - 1];
        changeMatrix[1][UniverseController.changeDuration - 1] = pd.PlanetList[ID].OrbitScale[ViewType - 1];
        ViewTypeChangeMatrix = changeMatrix;
    }

    /*
     * Updates the scale of the planets, accounts for earths mesh being a little odd. Should look into what is causing this problem
     */
    public void UpdateChangeValues()
    {
        if (ID == 3)
        {
            mesh.transform.localScale = Vector3.one * diameter * UniverseController.planetScale * 0.5f;
        }
        else
        {
            mesh.transform.localScale = Vector3.one * diameter * UniverseController.planetScale;
        }
        transform.localPosition = (MathPosition * UniverseController.orbitScale * privateOrbitScale);
    }
}








