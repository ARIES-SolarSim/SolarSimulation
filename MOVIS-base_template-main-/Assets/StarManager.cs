using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarManager : MonoBehaviour
{
    public GameObject starPrefab;
    public int starCount;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < starCount; i++)
        {
            float rRadius = Random.Range(20f, 50f);
            float rPhi = Random.Range(0, Mathf.PI / 2f);
            float rAngle = Random.Range(0, 2 * Mathf.PI);

            float x = rRadius * Mathf.Cos(rPhi) * Mathf.Cos(rAngle);
            float y = rRadius * Mathf.Sin(rPhi);
            float z = rRadius * Mathf.Cos(rPhi) * Mathf.Sin(rAngle);

            float rScale = Random.Range(0.05f, 0.3f);

            GameObject star = Instantiate(starPrefab, this.transform);
            star.transform.localPosition = new Vector3(x, y, z);
            star.transform.localScale = Vector3.one * rScale;
        }
    }
}
