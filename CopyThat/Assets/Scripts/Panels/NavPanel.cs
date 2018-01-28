using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NavPanel : Panel
{

    // MEMBERS

    public GameObject MapLocation;
   
    Module map;
    bool isPressed;

    //public delegate void ActivationDelegate();
    //public event ActivationDelegate OnStateChange;


    // Use this for initialization
    void Start()
    {
        //GameStateManager.Instance.TargetState = GAME_STATE.Flight;
        SetUIActive(false);
        isPressed = false;
        map = MapLocation.GetComponent<MapLogic>();

    }

    private void Update()
    {
        // Check to see if we are done if we are target
        if (GameStateManager.Instance.TargetState == GAME_STATE.Flight)
        {
            if (isPressed)
            {
                Debug.Log("Button On");
                if (map.currentState == Module.controlState.on)
                {
                    // Set to WinState
                    Debug.Log("You Win!");
                    GameStateManager.Instance.TargetState = GAME_STATE.Communication;
                    GameStateManager.Instance.GameState = GAME_STATE.Navigating;
                    GetComponentInParent<PanelManager>().CloseFlight();
                    GetComponentInParent<PanelManager>().OpenGameWon() ;

                    GameStateManager.Instance.AppState = APP_STATE.Won;
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
        isPressed = true;
    }

    private bool Completed()
    {
        return false;
    }

    private void OnDisable()
    {
        isPressed = false;
    }
}
