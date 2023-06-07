using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class dropdownmenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject image;
    public GameObject tide;
    
    public void DropdownSample(int index)
    {
        image = GameObject.Find("Image");
        tide = GameObject.Find("Tide");
        if (index == 0)
        {
            
            image.GetComponent<RectTransform>().anchoredPosition  = new Vector3(-385, -30, 0);
            tide.GetComponent<TideTempTwo>().myX = -5.401577f;
            tide.GetComponent<TideTempTwo>().myZ = -3.792488f;
            tide.GetComponent<TideTempTwo>().minY = 2+(51.5072f/90)*(3);

        }
        if (index == 1)
        {
            image.GetComponent<RectTransform>().anchoredPosition = new Vector3(-330, -75, 0);
            tide.GetComponent<TideTempTwo>().myX = -0.730737f;
            tide.GetComponent<TideTempTwo>().myZ = -6.559422f;
            tide.GetComponent<TideTempTwo>().minY = 2 + (30.0444f / 90) * (3);

        }
        if (index == 2)
        {
            image.GetComponent<RectTransform>().anchoredPosition = new Vector3(-270, 32, 0);
            tide.GetComponent<TideTempTwo>().myX = 3.621747f;
            tide.GetComponent<TideTempTwo>().myZ = 5.517513f;
            tide.GetComponent<TideTempTwo>().minY = 2 + (35.6762f / 90) * (3);

        }

    }
    void Start()
    {
        /*image = GameObject.Find("Image");
        var dropdown = transform.GetComponent<Dropdown>();
        dropdown.options.Clear();
        List<string> items = new List<string>();
        items.Add("London");
        items.Add("Macapa");
        items.Add("Blacksburg");

        foreach(var item in items)
        {
            dropdown.options.Add(new Dropdown.OptionData());
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
