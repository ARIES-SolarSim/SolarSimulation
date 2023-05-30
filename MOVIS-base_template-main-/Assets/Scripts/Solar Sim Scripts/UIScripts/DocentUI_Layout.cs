using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocentUI_Layout: MonoBehaviour
{ 
    public GameObject[] buttons;
    public Vector2 size;
    /// <summary>
    /// Selects the distance between the objects
    /// </summary>
    public int kerning; 

    //public string[] options = { "Left to Right", "Right to Left", "Top to Bottom", "Bottom to Top" };
    // Start is called before the first frame update
       
    [SerializeField]
    private enum Direction // unabashedly stolen from Slider
        // Dev note: This does absolutely nothing and has not been implemented. Ignore for now.
        // future S, I am so sorry 
    {
        /// <summary>
        /// From the left to the right
        /// </summary>
        LeftToRight,

        /// <summary>
        /// From the right to the left
        /// </summary>
        RightToLeft,

        /// <summary>
        /// From the bottom to the top.
        /// </summary>
        BottomToTop,

        /// <summary>
        /// From the top to the bottom.
        /// </summary>
        TopToBottom,
    }
    void Start()
    {
        RectTransform rt;
        //private Vector3 newPos = ;
        for(int i = 0; i < buttons.Length; i++)
        {
            rt = buttons[i].GetComponent<RectTransform>();
            rt.sizeDelta = size;
            rt.localPosition = new Vector3(i * (kerning + rt.rect.width), 0, 0);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}