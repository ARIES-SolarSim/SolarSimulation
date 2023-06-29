using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonOrbitTest1 : MonoBehaviour
{
    public Transform center;
    public float semiMajorAxis;
    public float semiMinorAxis;
    public float orbitSpeed;
    public float rotationSpeed;
    public float tiltAngle;

    private float currentOrbitAngle = 0f;
    private float currentRotationAngle = 0f;
    // Start is called before the first frame update
    void Start()
    {
        // Set initial position based on the desired semi-major and semi-minor axes
        float x = center.position.x + semiMajorAxis * Mathf.Cos(tiltAngle * Mathf.Deg2Rad);
        float z = center.position.z + semiMinorAxis * Mathf.Sin(tiltAngle * Mathf.Deg2Rad);
        transform.position = new Vector3(x, center.position.y, z);
    }

    // Update is called once per frame
    void Update()
    {
        if (!LobbyManager.userType)
        {
            // Calculate the new position on the elliptical orbit
            float x = center.position.x + semiMajorAxis * Mathf.Cos(currentOrbitAngle);
            float z = center.position.z + semiMinorAxis * Mathf.Sin(currentOrbitAngle);
            Vector3 newPosition = new Vector3(x, center.position.y, z);

            // Rotate around the center and update the position
            transform.RotateAround(center.position, Vector3.up, orbitSpeed * Time.deltaTime);
           // transform.LookAt(sun);

            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.position, center.position, Time.deltaTime, 0f));


            transform.position = newPosition;

            // Tilt the orbit by rotating around the x-axis
            Quaternion tiltRotation = Quaternion.Euler(tiltAngle, 0f, 0f);
            transform.position = tiltRotation * (transform.position - center.position) + center.position;

            // Update the current angle based on the rotation speed
            currentOrbitAngle += orbitSpeed * Time.deltaTime;
            currentRotationAngle += rotationSpeed * Time.deltaTime;

            // If the current angle exceeds a full circle, reset it
            if (currentOrbitAngle >= 2 * Mathf.PI)
            {
                currentOrbitAngle -= 2 * Mathf.PI;
            }

            // Apply rotation to the moon's own axis
            //transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}
