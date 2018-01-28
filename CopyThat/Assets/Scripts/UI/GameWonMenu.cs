using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class GameWonMenu : MonoBehaviour {

    Button[] buttons;
    Image background;

    public void Start()
    {
        buttons = GetComponentsInChildren<Button>();
        buttons[0].onClick.AddListener(() => PlayAgainButtonPressed());
        buttons[1].onClick.AddListener(() => ExitButtonPressed());
        background = GetComponentInChildren<Image>();
    }

    private void OnEnable()
    {
        GameStateManager.Instance.AppState = APP_STATE.Start; 
    }

    private void OnDisable()
    {
        GameStateManager.Instance.AppState = APP_STATE.Playing; 
    }

    public void PlayAgainButtonPressed()
    {
        Debug.Log("Play Again");
        GameStateManager.Instance.RestartGame(); 
        HideMenu();
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
