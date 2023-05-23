using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManagerLoading : MonoBehaviour

{
    public static GameManagerLoading instance;

    // Start is called before the first frame update
    private void awake()
    {

        SceneManager.LoadSceneAsync("Room 1", LoadSceneMode.Additive);
        
    }

    // Update is called once per frame
    public void LoadGame()
    {
        
    }
}
