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
            GameStateManager.Instance.GameTimer -= Time.deltaTime; 
        }
    }

    void SetTimerText()
    {
        int minutes = Mathf.FloorToInt(GameStateManager.Instance.GameTimer / 60F);
        int seconds = Mathf.FloorToInt(GameStateManager.Instance.GameTimer - minutes * 60);
        string formattedTime = string.Format("{0:0}:{1:00}", minutes, seconds);
        textElem.text = formattedTime; 
    }
}
