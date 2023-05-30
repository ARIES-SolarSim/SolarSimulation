using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocentUI_Layout: MonoBehaviour
{ 
    public GameObject[] buttons;
    public Vector2 size;
    public int kerning; 

    // Start is called before the first frame update
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