using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptManager : MonoBehaviour
{
    private string[] currentScript = { "1st Line", "expanded 1", "2nd Line", "Expanded 2" };
    private int scriptIter = 0;

    public TextAsset[] csvFiles;
    private string[][] agedScripts = new string[1][];

    //Font Uses 
    private int fontIter = 0;
    public int[] sizePresets = { 10, 11, 12 };
    private Text tc;

    private int ageIter = 0;

    private bool expanded = false;
    // Start is called before the first frame update
    void Start()
    {
        tc = this.GetComponent<Text>();
        ReadCSV();
        currentScript = agedScripts[0];
        tc.text = currentScript[0];
        //Handle reading here     
    }
    
    public void Forward()
    {   
        if (currentScript.Length > scriptIter + 2)
        {
            scriptIter += 2;
            expanded = false;
            tc.text = currentScript[scriptIter];
        }
    }

    public void Back()
    {
        if (scriptIter > 0)
        {
            scriptIter -= 2;
            expanded = false;
            tc.text = currentScript[scriptIter];
        }
    }

    /// <summary>
    /// This function will toggle whether or not the text contains it's expanded form
    /// The expanded form data is held in the odd indexed arrays 
    /// </summary>
    public void Expand()
    {
        if (expanded){ // It's already expanded out -> Shrink
            expanded = false;
            tc.text = currentScript[scriptIter];
            // Simply generate the evens again
        }

        else // It's not expanded -> Expand
        { 
            expanded = true;
            tc.text += "\n"+ currentScript[scriptIter + 1];
            // Add the associated Odd Script
        }
    }
    

    public void Age()
    {   
        ageIter = (ageIter + 1) % agedScripts.Length; // Loop through the scripts 
        currentScript = agedScripts[ageIter]; // change currentScript to the right age 
        scriptIter = 0; // Restart the script
        tc.text = currentScript[scriptIter]; // Display changes 
        expanded = false; // Mark that it is not expanded 
    }

    public void FontChange()
    {
        tc.fontSize = sizePresets[fontIter % sizePresets.Length];
        // Iterate through the size presets, set up at the stop of the currentScript
        fontIter = (fontIter + 1 % sizePresets.Length);
    }

    private void ReadCSV()
    {
        for (int i = 0; i < csvFiles.Length; i++)
        {
            agedScripts[i] = csvFiles[i].text.Split(new string[] { ";", "\n" }, System.StringSplitOptions.None);
        }
    }
}
