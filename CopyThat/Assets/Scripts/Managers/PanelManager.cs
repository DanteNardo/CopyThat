/// <summary>
/// Author: Dante Nardo
/// Last Modified: 1/28/2017
/// Purpose: PENLAL
/// </summary>
/// 
using UnityEngine; 

public class PanelManager : Singleton<PanelManager>
{
    public CommsPanel m_commsPanel;
    public SecurityPanel m_securityPanel;
    public EngineeringPanel m_engineeringPanel;
    public MapLogic m_flightPanel;
    public StartMenu m_startPanel;
    public GameOverMenu m_gameOverPanel;

    private GameObject comsObj;
    private GameObject secObj;
    private GameObject engObj;
    private GameObject flightObj;
    private GameObject startMenuObj;
    private GameObject gameOverMenuObj;
    private GameObject gameWonObj; 


    // Panel Access 
    public bool hasAccessFlight;
    public bool hasAccessEng;
    public bool hasAccessSec;

    private void Start()
    {
        m_commsPanel = GetComponentInChildren<CommsPanel>();
        m_securityPanel = GetComponentInChildren<SecurityPanel>();
        m_engineeringPanel = GetComponentInChildren<EngineeringPanel>();
        m_flightPanel = transform.GetChild(2).GetComponentInChildren<MapLogic>();
        m_startPanel = transform.GetComponentInChildren<StartMenu>(); 


        comsObj = transform.GetChild(1).gameObject;
        secObj = transform.GetChild(3).gameObject;
        engObj = transform.GetChild(0).gameObject;
        flightObj = transform.GetChild(2).gameObject;
        startMenuObj = transform.GetChild(4).gameObject;
        gameOverMenuObj = transform.GetChild(5).gameObject;
        gameWonObj = transform.GetChild(6).gameObject; 


        CloseComs();
        CloseSecurity();
        CloseEng();
        CloseFlight();
        CloseStart();
        CloseGameOver();
        CloseGameWon();
        OpenStart(); 
    }

    public void OpenComs()
    {
        GameStateManager.Instance.GameState = GAME_STATE.Communication;

        comsObj.SetActive(true); 
    }

    public void OpenSecurity()
    {
        if(hasAccessSec)
        {
            GameStateManager.Instance.GameState = GAME_STATE.Security; 
            secObj.SetActive(true);
        }
           
    }

    public void OpenEng()
    {
        if(hasAccessEng)
        {
            GameStateManager.Instance.GameState = GAME_STATE.Engineering;
            engObj.SetActive(true);
        }
            
    }

    public void OpenFlight()
    {
        if(hasAccessFlight)
        {
            GameStateManager.Instance.GameState = GAME_STATE.Flight;
            flightObj.SetActive(true);
        }
            
    }

    public void CloseComs()
    {
        GameStateManager.Instance.GameState = GAME_STATE.Navigating;
        comsObj.SetActive(false);
    }

    public void CloseSecurity()
    {
        GameStateManager.Instance.GameState = GAME_STATE.Navigating;

        secObj.SetActive(false);
    }

    public void CloseEng()
    {
        GameStateManager.Instance.GameState = GAME_STATE.Navigating;

        engObj.SetActive(false);
    }

    public void CloseFlight()
    {
        GameStateManager.Instance.GameState = GAME_STATE.Navigating;

        flightObj.SetActive(false);
    }



    // Menus
    public void OpenStart()
    {
        startMenuObj.SetActive(true); 
    }

    public void OpenGameOver()
    {
        gameOverMenuObj.SetActive(true); 
    }

    public void CloseStart()
    {
        startMenuObj.SetActive(false); 
    }

    public void CloseGameOver()
    {
        gameOverMenuObj.SetActive(false); 
    }

    public void OpenGameWon()
    {
        gameWonObj.SetActive(true); 
    }

    public void CloseGameWon()
    {
        gameWonObj.SetActive(false); 
    }
    
}
