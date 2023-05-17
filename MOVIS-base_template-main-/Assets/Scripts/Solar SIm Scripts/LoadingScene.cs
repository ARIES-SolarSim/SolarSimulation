using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class LoadingScene : MonoBehaviour
{
    public static LoadingScene Instance;

    [SerializeField] private GameObject _loaderCanvas;
    [SerializeField] private Image _progressbar;

    public void Awake() 
    {
        
        if (Instance == null)
        {
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
        var scene = SceneManager.LoadSceneAsync(sceneValue);
        scene.allowSceneActivation = false;

        _loaderCanvas.SetActive(true);

        do
        {

            await Task.Delay(100);
            _progressbar.fillAmount = scene.progress;

        } while (scene.progress < 0.9f);

        scene.allowSceneActivation = true;
        _loaderCanvas.SetActive(false);


    }
}
