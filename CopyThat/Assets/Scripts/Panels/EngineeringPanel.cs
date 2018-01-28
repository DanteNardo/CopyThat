using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class EngineeringPanel : MonoBehaviour {

    // MEMBERS / FIELDS
    Button[] buttons;


    int buttonOnePressCount = 0;
    bool leverPressed = false; 

    // METHODS
    // Use this for initialization
    void Start ()
    {
        buttons = GetComponentsInChildren<Button>();
        for (int i = 0; i < buttons.Length; i++)
        {
            if (i == 0)
            {
                buttons[i].onClick.AddListener(() => IncrementButtonOnePressed());
            }
            else if(i == buttons.Length-1)
            {
                buttons[i].onClick.AddListener(() => IncrementButtonOnePressed());
            }
            buttons[i].onClick.AddListener(() => PlayButtonSound());

        }
    }

    void IncrementButtonOnePressed()
    {
        buttonOnePressCount++; 
    }

    void LeverPress()
    {
        leverPressed = true; 
    }

    void PlayButtonSound()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
