using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private Rigidbody rb;

    public float sensX;
    public float sensY;
    public float moveSpeed;
    public float scrollSensitivity = 1f; // Sensitivity for scroll wheel adjustment
    public float minSpeed = 1f; // Minimum move speed limit
    public float maxSpeed = 20f; // Maximum move speed limit

    public Transform orientation;
    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode hideGroundPlane;
    public KeyCode switchUI;
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

    public GameObject UI;

    void Start()
    {
        if (sceneID == 1)
        {
            groundPlaneMaterial = groundPlane.GetComponent<Renderer>().material;
            startAlpha = groundPlaneMaterial.color.a;
        }
        else if (sceneID == 2)
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
        if (Input.GetKeyDown(switchUI))
        {
            UI.SetActive(!UI.activeSelf);
            Cursor.visible = UI.activeSelf;
            Cursor.lockState = (UI.activeSelf ? CursorLockMode.Confined : CursorLockMode.Locked);
        }

        // Mouse rotation
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        if (!UI.activeSelf)
        {
            yRot += mouseX;
            xRot -= mouseY;
            xRot = Mathf.Clamp(xRot, -90f, 90f);

            transform.rotation = Quaternion.Euler(xRot, yRot, 0);
            orientation.rotation = Quaternion.Euler(0, yRot, 0);
        }

        // Adjust moveSpeed based on scroll wheel input
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        moveSpeed += scrollInput * scrollSensitivity;
        moveSpeed = Mathf.Clamp(moveSpeed, minSpeed, maxSpeed);

        // Movement input
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");

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

        Vector3 moveDirection = orientation.forward * vInput + orientation.right * hInput + Vector3.up * zInput;
        moveDirection = moveDirection.normalized;

        float distance = Vector3.Distance(transform.position, Vector3.zero);
        float dotProduct = Vector3.Dot(moveDirection.normalized, transform.position.normalized);

        if (distance < maxDistance)
        {
            rb.AddForce(moveDirection * moveSpeed * 10f, ForceMode.Force);
        }
        else
        {
            if (dotProduct > 0)
            {
                float t = Mathf.Clamp((distance - maxDistance) / fallOffDist, 0f, 1f);
                rb.AddForce(moveDirection * moveSpeed * 10f * t, ForceMode.Force);
            }
            else
            {
                rb.AddForce(moveDirection * moveSpeed * 10f, ForceMode.Force);
            }
        }

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
        float duration = hideDuration;
        float currentTime = 0f;

        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(startAlpha, 0.0f, currentTime / duration);
            m.color = new Color(m.color.r, m.color.g, m.color.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }
        if (shouldSet)
            gb.SetActive(false);
    }

    IEnumerator FadeIn(Material m, GameObject gb, bool shouldSet)
    {
        if (shouldSet)
            gb.SetActive(true);
        float duration = hideDuration;
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
