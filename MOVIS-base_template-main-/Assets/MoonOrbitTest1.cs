using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonOrbitTest1 : MonoBehaviour
{
    public Transform center;
    public float radius;
    public float radiusSpeed;
    public float rotationSpeed;
    private float unit = .001f;

    private Vector3 axis;
    private Vector3 desiredPosition;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = (transform.position - center.position).normalized * radius + center.position;
        axis = Vector3.down;
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 centerCopy = center.position;
        centerCopy = new Vector3(centerCopy.x, centerCopy.y + unit, centerCopy.z);
        transform.RotateAround(centerCopy, axis, rotationSpeed * Time.deltaTime);
        desiredPosition = (transform.position - center.position).normalized * radius + centerCopy;
        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * radiusSpeed);

        if (Mathf.Abs(transform.position.y) >= 4)
        {
            unit = unit * -1f;
        }
    }
}
