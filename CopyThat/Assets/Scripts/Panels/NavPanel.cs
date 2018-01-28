using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NavPanel : Panel
{

    // MEMBERS

    public GameObject MapLocation;
    public GameObject LaunchButton;

    Module button;
    Module map;

    //public delegate void ActivationDelegate();
    //public event ActivationDelegate OnStateChange;


    // Use this for initialization
    void Start()
    {
        SetUIActive(false);

        button = LaunchButton.GetComponent<SwitchBehavior>();
        map = MapLocation.GetComponent<MapLogic>();

    }

    private void Update()
    {
        // Check to see if we are done if we are target
        if (GameStateManager.Instance.TargetState == GAME_STATE.Flight)
        {
            if (button.currentState == Module.controlState.on)
            {
                Debug.Log("Button On");
                if (map.currentState == Module.controlState.on)
                {
                    // Set to WinState
                    Debug.Log("You Win!");
                    GameStateManager.Instance.TargetState = GAME_STATE.Communication;
                    GameStateManager.Instance.GameState = GAME_STATE.Navigating;
                    GameStateManager.Instance.AppState = APP_STATE.Won;
                    PanelManager.Instance.m_commsPanel.NextInstruction();
                }
                else
                {
                    // Set to LoseState
                    Debug.Log("You Lose!");
                    GameStateManager.Instance.AppState = APP_STATE.Lost;
                }
            }

        }
    }

    /// <summary>
    /// Button event to determine if the goal conditions for this panel have been met
    /// </summary>
    public void TestForActivation()
    {

    }

    private bool Completed()
    {
        return false;
    }
}
