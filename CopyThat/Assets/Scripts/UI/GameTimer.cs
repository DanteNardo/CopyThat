using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class GameTimer : MonoBehaviour {

    // Member vars
    Text textElem; 

	// Use this for initialization
	void Start () {
        textElem = GetComponent<Text>(); 
    }
	
	// Update is called once per frame
	void Update () {
        UpdateGameTimer();
        SetTimerText(); 
	}

    void UpdateGameTimer()
    {
        
        if(GameStateManager.Instance.AppState == APP_STATE.Playing)
        {
            if (GameStateManager.Instance.GameTimer <= 1)
            {
                GameStateManager.Instance.AppState = APP_STATE.Lost;
                PanelManager panelManager = GetComponentInParent<PanelManager>();
                panelManager.CloseEng();
                panelManager.CloseComs();
                panelManager.CloseFlight();
                panelManager.CloseSecurity(); 
                panelManager.OpenGameOver(); 
            }
            else
            {
                GameStateManager.Instance.GameTimer -= Time.deltaTime;
            }
            
        }
    }

    void SetTimerText()
    {
        int minutes = Mathf.FloorToInt(GameStateManager.Instance.GameTimer / 60F);
        int seconds = Mathf.FloorToInt(GameStateManager.Instance.GameTimer - minutes * 60);
        int milliseconds = Mathf.FloorToInt((GameStateManager.Instance.GameTimer - minutes * 60) * 100 - (seconds * 100));
        //Debug.Log(GameStateManager.Instance.GameTimer);
        string formattedTime = string.Format("{0:0}:{1:00}:{2:00}", minutes, seconds, milliseconds);
        textElem.text = formattedTime; 
    }
}
