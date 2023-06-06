using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DocentUI_Manager: MonoBehaviour
{
    // This script needs to change the color of the active scene/view changer
    // This script also needs to set which top row buttons are in view (So it needs two Game Objects Tuples) 
    // It needs to read the view type observer active scene 
    // Start is called before the first frame update

    public GameObject ViewTypeObserverObject; 
    public (GameObject, GameObject)[] roomSpecificFunctions; // Tuple containing room specific Functions
    public GameObject[] sceneChangers; // Give each of the buttons for color changing

    public Color activeColor;
    public Color inactiveColor;

    private ViewTypeObserver vTO;


    void Start()
    {
        vTO = ViewTypeObserverObject.GetComponent<ViewTypeObserver>(); //All for making the code more readable 

    }
    // Update is called once per frame
    public void changeState(int oldScene, int newScene)
    {   //Interfaced through VTO 
        // Button Activation
        roomSpecificFunctions[oldScene].Item1.SetActive(false); // Set the old tuple inactive
        roomSpecificFunctions[oldScene].Item2.SetActive(false);
        sceneChangers[oldScene].gameObject.GetComponent<Image>().color = inactiveColor;

        roomSpecificFunctions[newScene].Item1.SetActive(true); // Activate the new one 
        roomSpecificFunctions[newScene].Item2.SetActive(true);
        sceneChangers[newScene].gameObject.GetComponent<Image>().color = activeColor;

        ///TODO: Currently Oldscene is mapped to the Y value, which is the view, not scene. If implemented, this is required to be fixed.
    }
}
