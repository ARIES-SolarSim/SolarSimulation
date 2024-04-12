using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraMovement : MonoBehaviour
{
    public Transform target; // Target to follow
    public float smoothSpeed = 0.125f; // Smoothing factor
    public Vector3 offset; // Offset from the target

    // Velocity reference for SmoothDamp
    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        // Desired position with offset
        Vector3 desiredPosition = target.position + offset;

        // Smoothly move the camera towards the desired position
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);

        // Optional: Smooth look at target
        SmoothLookAt();
    }

    void SmoothLookAt()
    {
        // Calculate direction from camera to target
        Vector3 direction = target.position - transform.position;

        // Smoothly rotate towards the target point.
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothSpeed * Time.deltaTime);
    }
}
