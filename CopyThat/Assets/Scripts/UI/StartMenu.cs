using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class StartMenu : MonoBehaviour {

    Button[] buttons;
    Image background;
    Image title;
    public Sprite credits;

    public void Start()
    {
        buttons = GetComponentsInChildren<Button>();
        buttons[0].onClick.AddListener(() => PlayButtonPressed());
        buttons[1].onClick.AddListener(() => CreditsButtonPressed());
        buttons[2].onClick.AddListener(() => ExitButtonPressed());
        background = GetComponentsInChildren<Image>()[0];
        title = GetComponentsInChildren<Image>()[1];
    }

    public void PlayButtonPressed () {
        Debug.Log("Play"); 
        GameStateManager.Instance.AppState = APP_STATE.Playing;
        
        HideMenu(); 
	}

    public void CreditsButtonPressed()
    {
        Debug.Log("Credits!");
        title.sprite = credits;
    }

    public void ExitButtonPressed()
    {
        Debug.Log("Quit!"); 
        GameStateManager.Instance.AppState = APP_STATE.Quit;
        Application.Quit(); 
    }

    public void HideMenu()
    {
        foreach(var b in buttons)
        {
            b.gameObject.SetActive(false); 
        }
        background.gameObject.SetActive(false);
        GetComponentInParent<PanelManager>().CloseStart();
    }

}
