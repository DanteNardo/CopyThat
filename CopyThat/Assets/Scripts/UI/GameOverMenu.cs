﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class GameOverMenu : MonoBehaviour {

    Button[] buttons;
    Image background;

    public void Start()
    {
        
    }

    private void OnEnable()
    {
        buttons = GetComponentsInChildren<Button>();
        buttons[0].onClick.AddListener(() => PlayAgainButtonPressed());
        buttons[1].onClick.AddListener(() => ExitButtonPressed());
        background = GetComponentInChildren<Image>();
        //GameStateManager.Instance.AppState = APP_STATE.Start;
    }

    private void OnDisable()
    {
        //GameStateManager.Instance.AppState = APP_STATE.Playing;
    }

    public void PlayAgainButtonPressed()
    {
        Debug.Log("Play Again");
        GetComponentInParent<PanelManager>().CloseGameOver();
        GetComponentInParent<PanelManager>().CloseGameWon();
        GameStateManager.Instance.RestartGame(); 
        //HideMenu();

    }

    public void ExitButtonPressed()
    {
        Debug.Log("Quit!");
        GameStateManager.Instance.AppState = APP_STATE.Quit;
        Application.Quit();
    }

    public void HideMenu()
    {
        foreach (var b in buttons)
        {
            b.gameObject.SetActive(false);
        }
        background.gameObject.SetActive(false);
        GetComponentInParent<PanelManager>().CloseGameOver();
        GetComponentInParent<PanelManager>().CloseGameWon(); 
    }
}
