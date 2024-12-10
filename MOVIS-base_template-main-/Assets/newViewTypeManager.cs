using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class newViewTypeManager : MonoBehaviour
{
    private static string[] levelNamesPC = { "Lobby", "Room1PC", "Room2PC", "Room3PC", "Room4PC", "Room5PC" }; // names of each scene, in order (update as we add more)

    public Text check;
    public bool pressedTwice = false;
    private int buttonNum;

    public void changeViewType(int index)
    {
        if (pressedTwice & buttonNum == index)
        {
            SceneManager.LoadScene(levelNamesPC[index]);
        }
        else
        {
            buttonNum = index;
            StartCoroutine(Check());
        }
    }

    IEnumerator Check()
    {
        check.gameObject.SetActive(true);
        pressedTwice = true;
        yield return new WaitForSeconds(5f);
        check.gameObject.SetActive(false);
        pressedTwice = false;
    }
}
