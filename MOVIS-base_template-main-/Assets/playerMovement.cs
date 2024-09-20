using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private Rigidbody rb;

    public float sensX;
    public float sensY;
    public float moveSpeed;

    public Transform orientation;
    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode hideGroundPlane;
    public GameObject groundPlane;
    public GameObject bigEarth;
    public float hideDuration;
    public float maxDistance;
    public float fallOffDist;

    float xRot;
    float yRot;

    float hInput;
    float vInput;
    float zInput;
    Vector3 moveDirection;

    public float drag;

    private bool isFadingOut = true;
    private float startAlpha;

    public int sceneID;

    Material groundPlaneMaterial;

    void Start()
    {
        if (sceneID == 1)
        {
            groundPlaneMaterial = groundPlane.GetComponent<Renderer>().material;
            startAlpha = groundPlaneMaterial.color.a;
        }
        else if(sceneID == 2)
        {
            startAlpha = bigEarth.GetComponent<Renderer>().material.color.a;
        }
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Mouse rotation
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        // Update rotation based on mouse movement
        yRot += mouseX;
        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -90f, 90f);

        // Apply rotation to player object and orientation
        transform.rotation = Quaternion.Euler(xRot, yRot, 0);
        orientation.rotation = Quaternion.Euler(0, yRot, 0);

        // Movement input
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");

        // Elevation input (Q/E for up/down)
        if (Input.GetKey(upKey))
        {
            zInput++;
        }
        else if (Input.GetKey(downKey))
        {
            zInput--;
        }
        if (Input.GetKeyDown(hideGroundPlane))
        {
            if (sceneID == 1)
                ToggleFade(groundPlaneMaterial);
            else if (sceneID == 2)
                ToggleFade(bigEarth.GetComponent<Renderer>().material);
                
        }

        // Move direction relative to player's current orientation
        Vector3 moveDirection = orientation.forward * vInput + orientation.right * hInput + Vector3.up * zInput;

        // Normalize the moveDirection so movement speed remains constant
        moveDirection = moveDirection.normalized;

        // Handle movement based on distance to origin
        float distance = Vector3.Distance(transform.position, Vector3.zero);
        float dotProduct = Vector3.Dot(moveDirection.normalized, transform.position.normalized);

        if (distance < maxDistance)
        {
            // Apply force when within range
            rb.AddForce(moveDirection * moveSpeed * 10f, ForceMode.Force);
        }
        else
        {
            if (dotProduct > 0) // Moving away
            {
                float t = Mathf.Clamp((Vector3.Distance(transform.position, Vector3.zero) - maxDistance) / fallOffDist, 1f, 0f);
                rb.AddForce(moveDirection * moveSpeed * 10f * t, ForceMode.Force);
            }
            else // Moving back towards origin
            {
                rb.AddForce(moveDirection * moveSpeed * 10f, ForceMode.Force);
            }
        }

        // Apply drag and reset zInput for the next frame
        rb.drag = drag;
        zInput = 0;
    }

    public void ToggleFade(Material m)
    {
        if (isFadingOut)
        {
            StartCoroutine(FadeOut(m, groundPlane, sceneID != 2));
        }
        else
        {
            StartCoroutine(FadeIn(m, groundPlane, sceneID != 2));
        }
        isFadingOut = !isFadingOut;
    }

    IEnumerator FadeOut(Material m, GameObject gb, bool shouldSet)
    {
        float duration = hideDuration; // Duration in seconds
        float currentTime = 0f;

        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(startAlpha, 0.0f, currentTime / duration);
            m.color = new Color(m.color.r, m.color.g, m.color.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }
        if(shouldSet)
            gb.SetActive(false);
    }

    IEnumerator FadeIn(Material m, GameObject gb, bool shouldSet)
    {
        if(shouldSet)
            gb.SetActive(true);
        float duration = hideDuration; // Duration in seconds
        float currentTime = 0f;

        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(0.0f, startAlpha, currentTime / duration);
            m.color = new Color(m.color.r, m.color.g, m.color.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }
        m.color = new Color(m.color.r, m.color.g, m.color.b, startAlpha);
    }
}
