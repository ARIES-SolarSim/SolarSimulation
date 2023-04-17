using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamburgerMenu : MonoBehaviour
{
    public GameObject hamburgerIcon;
    public GameObject panel;

    // Start is called before the first frame update
    void Start()
    {
        hamburgerIcon.SetActive(true);
        //panel.SetActive(false);
    }

    public void openMenu()
    {
        hamburgerIcon.SetActive(false);
        panel.SetActive(true);
    }

    public void closeMenu()
    {
        hamburgerIcon.SetActive(true);
        panel.SetActive(false);
    }
}
