using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transaparency : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Renderer>().material.color = new Color(this.GetComponent<Renderer>().material.color.r, this.GetComponent<Renderer>().material.color.g, this.GetComponent<Renderer>().material.color.b, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
