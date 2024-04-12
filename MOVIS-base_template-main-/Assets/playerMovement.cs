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
    public float maxDistance;

    float xRot;
    float yRot;

    float hInput;
    float vInput;
    float zInput;
    Vector3 moveDirection;

    public float drag;

    // Start is called before the first frame update

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
        if(Input.GetKey(upKey))
        {
            zInput++;
        }
        else if (Input.GetKey(downKey))
        {
            zInput--;
        }

        yRot += mouseX;
        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -90f, 90f);
        transform.rotation = Quaternion.Euler(xRot, yRot, 0);
        orientation.rotation = Quaternion.Euler(0, yRot, 0);

        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");
        moveDirection = orientation.forward * vInput + orientation.right * hInput + orientation.up * zInput;
        //rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        
        float distance = Vector3.Distance(transform.position, Vector3.zero);

        // Calculate the dot product
        float dotProduct = Vector3.Dot(moveDirection.normalized, transform.position.normalized);


        //Log.Debug(distance);
        if (distance < maxDistance)
        {
            // Object is within 25 meters, apply your desired force
            //Vector3 moveDirection = -transform.position;
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else
        {
            //Debug.Log(dotProduct);
            // Object is beyond 25 meters, apply force towards the origin
            //Vector3 forceDirection = -transform.position; // Direction from object to origin
            if(dotProduct > 0) //Moving Away
            {
                float t = Mathf.Clamp((Vector3.Distance(transform.position, Vector3.zero) - maxDistance) / 10f, 1f, 0f);  // Normalize t to 0-1
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f * t, ForceMode.Force);
            }
            else //Moving Back
            {
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
            }
        }
        rb.drag = drag;
        zInput = 0;
    }
}
