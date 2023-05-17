using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshScaler : MonoBehaviour
{
    private Vector3[] scales = {new Vector3(0.000003476238745f, 0.000003476238745f, 0.000003476238745f),
        //Vector3.one };
        new Vector3(0.05079997257f, 0.05079997257f, 0.05079997257f)};
    public static int view = 0;

    public float trailTime;
    private bool isChanging = false;
    public int steps = 0;
    public TrailRenderer tr;

    // Start is called before the first frame update
    void Start()
    {
        view = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(isChanging)
        {
            tr.time = 0;
            int targetView = (view == 1) ? 0 : 1;
            transform.localScale = Vector3.Lerp(scales[view], scales[targetView], (steps * 1f) / (UniverseController.changeDuration * 1f)) * UniverseController.planetScale;
            steps++;
           // Debug.Log("Changing");
        }

        else
        {
            tr.time = trailTime;
        }
        
        //transform.localScale = Vector3.one * scales[view - 1].x * UniverseController.planetScale;
    }

    public void changing()
    {
        isChanging = true;
       // Debug.Log("Set true");
    }

    public void doneChanging()
    {
        isChanging = false;
        //Debug.Log("Set false");
    }
}
