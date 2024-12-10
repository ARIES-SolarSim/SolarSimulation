using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartExperience : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void getThisShowOnTheRoad()
    {
        Debug.Log("trying to change");
        SceneManager.LoadScene(1);
    }
}
