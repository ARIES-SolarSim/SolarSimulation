using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocentUI_Layout : MonoBehaviour
{
    public Vector2 size;
    /// <summary>
    /// Selects the distance between the objects
    /// </summary>
    public int kerning;

    public Direction dir;

    //public string[] options = { "Left to Right", "Right to Left", "Top to Bottom", "Bottom to Top" };
    // Start is called before the first frame update   
    public enum Direction // shamelessly stolen from Slider
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
        GameObject[] buttons = new GameObject[this.transform.childCount];
        RectTransform rt;
        for (int buttonIter = 0; buttonIter < this.transform.childCount; buttonIter++)
        {   // Getting each of the children 
            buttons[buttonIter] = transform.GetChild(buttonIter).gameObject;
        }

        for (int i = 0; i < buttons.Length; i++) //Do it for each button
        {
            rt = buttons[i].GetComponent<RectTransform>();
            rt.sizeDelta = size;
            switch (dir)
            {
                case Direction.LeftToRight:
                    rt.localPosition = new Vector3(i * (kerning + rt.rect.width), 0, 0);
                    break;

                case Direction.RightToLeft:
                    rt.localPosition = new Vector3(-i * (kerning + rt.rect.width), 0, 0);
                    break;
                // Vertical is untested, if they're backwards, oops! 
                case Direction.BottomToTop:
                    rt.localPosition = new Vector3(0, i * (kerning + rt.rect.height), 0);
                    break;

                case Direction.TopToBottom:
                    rt.localPosition = new Vector3(0, -i * (kerning + rt.rect.height), 0);
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}