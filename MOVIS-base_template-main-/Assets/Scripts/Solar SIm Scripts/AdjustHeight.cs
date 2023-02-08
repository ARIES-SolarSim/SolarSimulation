using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustHeight : MonoBehaviour
{
    public GameObject universe;
    [Range(0f, 6f)]
    public float height;

    // Start is called before the first frame update
    void Start()
    {
        height = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        universe.transform.position = new Vector3(0, height, 0);
    }
}
