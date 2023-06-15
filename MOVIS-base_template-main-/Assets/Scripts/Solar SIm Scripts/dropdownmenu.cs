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
        //image = GameObject.Find("Image");
        //tide = GameObject.Find("Tide");
        //United Kingdom
        if (index == 0)
        {
            tide.GetComponent<TideTempTwo>().enabled = false;
            tide.GetComponent<TideTempTwo>().enabled = true;
            image.GetComponent<RectTransform>().anchoredPosition  = new Vector3(-420, -45, 0);
            tide.GetComponent<TideTempTwo>().myX = -5.401577f;
            tide.GetComponent<TideTempTwo>().myZ = -3.792488f;
            tide.GetComponent<TideTempTwo>().minY = 8 + (51.5072f/90)*(12);

        }
        //Egypt
        /*
        if (index == 1)
        {
            tide.GetComponent<TideTempTwo>().enabled = false;
            tide.GetComponent<TideTempTwo>().enabled = true;
            image.GetComponent<RectTransform>().anchoredPosition = new Vector3(-380, -110, 0);
            tide.GetComponent<TideTempTwo>().myX = -0.730737f;
            tide.GetComponent<TideTempTwo>().myZ = -6.559422f;
            tide.GetComponent<TideTempTwo>().minY = 8 + (30.0444f / 90) * (12);

        }
        */
        //Japan
        if (index == 1)
        {
            tide.GetComponent<TideTempTwo>().enabled = false;
            tide.GetComponent<TideTempTwo>().enabled = true;
            image.GetComponent<RectTransform>().anchoredPosition = new Vector3(-280, 40, 0);
            tide.GetComponent<TideTempTwo>().myX = 3.621747f;
            tide.GetComponent<TideTempTwo>().myZ = 5.517513f;
            tide.GetComponent<TideTempTwo>().minY = 8 + (35.6762f / 90) * (12);

        }
        //India
        if (index == 2)
        {
            //Destroy(tide.GetComponent<TideTempTwo>());
            //tide.AddComponent<TideTempTwo>();
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            tide.GetComponent<TideTempTwo>().enabled = false;
            tide.GetComponent<TideTempTwo>().enabled = true;
            image.GetComponent<RectTransform>().anchoredPosition = new Vector3(-290, -85, 0);
            tide.GetComponent<TideTempTwo>().myX = 3.646949f;
            tide.GetComponent<TideTempTwo>().myZ = -4.234735f;
            tide.GetComponent<TideTempTwo>().minY = 8 + (8.5241f / 90) * (12);

        }
        //North Pole
        if (index == 3)
        {
            //Destroy(tide.GetComponent<TideTempTwo>());
            //tide.AddComponent<TideTempTwo>();
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            tide.GetComponent<TideTempTwo>().enabled = false;
            tide.GetComponent<TideTempTwo>().enabled = true;
            image.GetComponent<RectTransform>().anchoredPosition = new Vector3(-390, 20, 0);
            tide.GetComponent<TideTempTwo>().myX = 4.203157f;
            tide.GetComponent<TideTempTwo>().myZ = -5.088562f;
            tide.GetComponent<TideTempTwo>().minY = 8 + (90.00f / 90) * (12);

        }
        //Singapore
        if (index == 4)
        {
            //Destroy(tide.GetComponent<TideTempTwo>());
            //tide.AddComponent<TideTempTwo>();
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            tide.GetComponent<TideTempTwo>().enabled = false;
            tide.GetComponent<TideTempTwo>().enabled = true;
            image.GetComponent<RectTransform>().anchoredPosition = new Vector3(-260, -40, 0);
            tide.GetComponent<TideTempTwo>().myX = 4.203157f;
            tide.GetComponent<TideTempTwo>().myZ = -5.088562f;
            tide.GetComponent<TideTempTwo>().minY = 8 + (1.3521f / 90) * (12);

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
