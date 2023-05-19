using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using Photon.Pun;

public class LoadingScene : MonoBehaviour
{
 

    public void LoadScene(string sceneValue)
    {


        SceneManager.LoadScene(sceneValue);




    }


    
  

   
}
