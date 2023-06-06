using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class dropdownmenu : MonoBehaviour
{
    public GameObject image;
    // Start is called before the first frame update
    void Start()
    {
        image = GameObject.Find("Image");
        var dropdown = transform.GetComponent<Dropdown>();
        dropdown.options.Clear();
        List<string> items = new List<string>();
        items.Add("London");
        items.Add("Macapa");
        items.Add("Blacksburg");

        foreach(var item in items)
        {
            dropdown.options.Add(new Dropdown.OptionData());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
