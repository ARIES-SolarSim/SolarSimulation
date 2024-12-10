using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TidesPCPlayer : MonoBehaviour
{
    public KeyCode switchUI;
    public GameObject UI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(switchUI))
        {
            UI.SetActive(!UI.activeSelf);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;// (UI.activeSelf ? CursorLockMode.Confined : CursorLockMode.Locked);
        }
    }
}
