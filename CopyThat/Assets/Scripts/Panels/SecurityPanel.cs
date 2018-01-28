using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SecurityPanel : Panel {

    // MEMBERS
    public string usernameGoal = "IAmTheCaptain";
    public string passwordGoal = "Password123";

    private GameObject usernameInput;
    private GameObject passwordInput;

    //public delegate void ActivationDelegate();
    //public event ActivationDelegate OnStateChange;


    // Use this for initialization
    void Start () {
        SetUIActive(false);
        
        for (int i = 0; i < panelUI.transform.childCount; i++)
        {
            if (panelUI.transform.GetChild(i).name == "Username")
            {
                usernameInput = panelUI.transform.GetChild(i).gameObject;
            }
            else if (panelUI.transform.GetChild(i).name == "Password")
            {
                passwordInput = panelUI.transform.GetChild(i).gameObject;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Button event to determine if the goal conditions for this panel have been met
    /// </summary>
    public void TestForActivation()
    {
        if (usernameInput.GetComponent<InputField>().text == usernameGoal
            && passwordInput.GetComponent<InputField>().text == passwordGoal)
        {
            GameStateManager.Instance.GameState = GAME_STATE.Navigating;
            SetUIActive(false);
        }
    }
}
