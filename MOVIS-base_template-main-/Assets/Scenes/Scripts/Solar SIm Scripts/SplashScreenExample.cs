using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class SplashScreenExample : MonoBehaviour
{
    IEnumerator Start()
    {
        Debug.Log("Showing splash screen");
        SplashScreen.Begin();
        while (!SplashScreen.isFinished)
        {
            SplashScreen.Draw();
            yield return null;
        }
        Debug.Log("Finished showing splash screen");
    }
}
