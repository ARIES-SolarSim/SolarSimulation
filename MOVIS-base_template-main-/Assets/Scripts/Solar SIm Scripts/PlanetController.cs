using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlanetController : MonoBehaviour
{
    public float diameter; //The demonstrated diameter of the planet. This value does not alter the forces of the planet.
    public float mass; //The simulate mass of the planet. This value should NOT change.
    public Vector3 InitialPosition; //The starting position for the planet
    public Vector3 InitialVelocity; //The starting velocity for the planet
                                    //public float tiltAngle; , using the z rotation for this
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
    public float tiltAngle;

    /*
     * The awake initiates several values referencing from the virtualController.
     *
     * MathPos should be used for any velocity, position, or acceleration calculations and is not affected by scale
     * Rigidbody's position values should change due to scale and are not used in any calulations.
     */
    void Awake()
    {
        mesh.transform.localEulerAngles = new Vector3(tiltAngle, 0f, 0f);
        if (ID == 10)
        {
            InitialPosition = GetComponent<PlanetBuilderInterface>().getDistFromSun();
            InitialVelocity = new Vector3(0f, 0f, GetComponent<PlanetBuilderInterface>().getVelocity());
            mass = GetComponent<PlanetBuilderInterface>().getMass();
        }
        transform.localPosition = InitialPosition * UniverseController.orbitScale;
        MathPosition = transform.localPosition * privateOrbitScale;
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
        PhotonView view = GetComponent<PhotonView>();
        mesh.transform.Rotate(0f, rotationSpeed, 0f);
        if (FindObjectOfType<UniverseController>().isPlanetBuilder)
        {
            if (ID != 0 && ID != 4)
            {

                if (!UniverseController.orbiting)

                {
                    tr.time = trailTime;
                }
                else
                {
                    tr.time = 0;
                }
            }
            updateScale();
        }
        else
        {
            if (ID != 0 && ID != 4 && !LobbyManager.userType)
            {
                if (trailObserver.transform.localPosition.z == 1)
                {
                    view.RPC("ClearTrail", RpcTarget.All);
                }
                else
                {
                    view.RPC("StartTrail", RpcTarget.All);

                }
            }
        }
    }

    [PunRPC]
    public void ClearTrail()
    {
        tr.time = 0;
    }
    [PunRPC]
    public void StartTrail()
    {
        tr.time = trailTime;
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
        if (ID == 5)
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
        transform.localPosition = (MathPosition - FindObjectOfType<UniverseController>().cameraLockedPlanet.MathPosition) * UniverseController.orbitScale * privateOrbitScale;
    }

    //Placeholder
    public void updateScale()
    {
        if (ID == 3)
        {
            mesh.transform.localScale = Vector3.one * diameter * UniverseController.planetScale * 0.5f;
        }
        else if (ID == 0)
        {
            mesh.transform.localScale = Vector3.one * diameter * UniverseController.planetScale * 0.04f;
        }
        else
        {
            mesh.transform.localScale = Vector3.one * diameter * UniverseController.planetScale;
        }

    }
}