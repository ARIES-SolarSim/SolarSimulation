using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabMovement : MonoBehaviour
{

    public float min;
    public float max;
    // Use this for initialization
    void Start () {
        min = transform.position.x;
        max=transform.position.x + 10f;
   
    }
   
    // Update is called once per frame
    void Update () {
       
       
        transform.position =new Vector3(Mathf.PingPong(Time.time*2,max-min)+min, transform.position.y, transform.position.z);
        transform.localEulerAngles = new Vector3(180, Mathf.PingPong((Time.time *7) * -10, 10), 180);
       
    }
}
