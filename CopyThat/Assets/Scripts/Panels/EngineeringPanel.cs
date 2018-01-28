using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class EngineeringPanel : MonoBehaviour {

    // MEMBERS / FIELDS
    Button[] buttons;
    Image statusColor; 

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
            else if(i == buttons.Length - 1)
            {
                //buttons[i].onClick.AddListener(() => LeverPress());
            }
            buttons[i].onClick.AddListener(() => PlayButtonSound());

        }

        Image[] images = GetComponentsInChildren<Image>();
        statusColor = images[images.Length - 1]; 
    }

    void IncrementButtonOnePressed()
    {
        buttonOnePressCount++;
    }

    public void LeverPress()
    {
        leverPressed = true; 
    }

    private void OnDisable()
    {
        leverPressed = false;
        buttonOnePressCount = 0;
        statusColor.color = Color.red; 
    }

    void PlayButtonSound()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (buttonOnePressCount > 2 && leverPressed)
        {
            statusColor.color = Color.green;
        }

        // Check to see if we are done if we are target
        if (GameStateManager.Instance.TargetState == GAME_STATE.Engineering)
        {
            if (Completed())
            {
                //PanelManager.Instance.CloseEng();
                GameStateManager.Instance.TargetState = GAME_STATE.Communication;
                PanelManager.Instance.m_commsPanel.NextInstruction();
            }
        }
    }

    private bool Completed()
    {
        if(leverPressed == true  && buttonOnePressCount > 2)
        {
            return true; 
        }
        return false;
    }
}
