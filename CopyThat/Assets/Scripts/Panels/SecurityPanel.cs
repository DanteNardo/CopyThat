using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SecurityPanel : Panel {

    // MEMBERS
    public string usernameGoal = "admin873";
    public string passwordGoal = "gnortsmra";

    private bool completed = false;
    private GameObject usernameInput;
    private GameObject passwordInput;

    //public delegate void ActivationDelegate();
    //public event ActivationDelegate OnStateChange;


    // Use this for initialization
    void Start () {
        SetUIActive(false);
        completed = false;
        
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

    private void Update()
    {
        // Check to see if we are done if we are target
        if (GameStateManager.Instance.TargetState == GAME_STATE.Security)
        {
            if (completed)
            {
                PanelManager.Instance.CloseSecurity();
                GameStateManager.Instance.TargetState = GAME_STATE.Communication;
                PanelManager.Instance.m_commsPanel.NextInstruction();
                completed = false;
            }
        }
    }

    private void OnDisable()
    {
        if (usernameInput != null)
        {
            usernameInput.GetComponent<InputField>().text = "";
        }
        if (passwordInput != null)
        {
            passwordInput.GetComponent<InputField>().text = "";
        }
        completed = false;
    }

    /// <summary>
    /// Button event to determine if the goal conditions for this panel have been met
    /// </summary>
    public void TestForActivation()
    {
        if (usernameInput.GetComponent<InputField>().text.ToLower() == usernameGoal
            && passwordInput.GetComponent<InputField>().text.ToLower() == passwordGoal)
        {
            completed = true;
        }
    }
}
