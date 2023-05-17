using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using Photon.Pun;

public class LoadingScene : MonoBehaviour
{
    public static LoadingScene Instance;

    [SerializeField] private GameObject _loaderCanvas;
    [SerializeField] private Image _progressbar;

    public void Awake() 
    {
        
        if (Instance == null)
        {
            _loaderCanvas.SetActive(true);
            Debug.Log("here");
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public async void LoadScene(int sceneValue)
    {
        
        AsyncOperation scene = SceneManager.LoadSceneAsync(sceneValue);
        scene.allowSceneActivation = false;

        

        do
        {

            await Task.Delay(100);
            _progressbar.fillAmount = scene.progress;

        } while (scene.progress < 0.9f);

        scene.allowSceneActivation = true;
        _loaderCanvas.SetActive(false);


    }
}
