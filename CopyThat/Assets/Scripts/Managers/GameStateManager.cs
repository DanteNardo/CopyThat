using UnityEngine;

public enum GAME_STATE
{
    Navigating, 
    Communication, 
    Engineering, 
    Security, 
    Flight
}

public enum APP_STATE
{
    Start, 
    Playing, 
    Won, 
    Lost,
    Restart, 
    Quit
}

public class GameStateManager : Singleton<GameStateManager>
{
    protected GameStateManager () { } // // guarantee this will be always a singleton only - can't use the constructor!

    // Global Variables and Functions here 
    private const float GAME_TIME_LIMIT = 600;

    private GAME_STATE state = GAME_STATE.Navigating;
    public GAME_STATE GameState
    {
        get { return state; }
        set
        {
            if (state == value) return;
            state = value;
            if (OnStateChange != null)
                OnStateChange(state);
        }
    }
    public delegate void OnVariableChangeDelegate(GAME_STATE pState);
    public event OnVariableChangeDelegate OnStateChange;

    private GAME_STATE targetState = GAME_STATE.Communication;
    public GAME_STATE TargetState
    {
        get { return targetState; }
        set
        {
            if (targetState == value) return;
            targetState = value;
            if (OnTargetStateChange != null)
                OnTargetStateChange(targetState);
        }
    }
    public delegate void OnTargetChangeDelegate(GAME_STATE pState);
    public event OnTargetChangeDelegate OnTargetStateChange;



    private APP_STATE appState = APP_STATE.Start;
    public APP_STATE AppState
    {
        get { return appState; }
        set
        {
            if (appState == value) return;
            appState = value;
        }
    }

    private float gameTimer = GAME_TIME_LIMIT;
    public float GameTimer
    {
        get { return gameTimer; }
        set
        {
            gameTimer = value; 
        }
    }


    // Room conditions 
    private bool securityComplete = false;
    public bool SecurityComplete
    {
        get { return securityComplete; }
        set
        {
            securityComplete = value;
        }
    }

    private bool engineeringComplete = false;
    public bool EngineeringComplete
    {
        get { return engineeringComplete; }
        set
        {
            engineeringComplete = value;
        }
    }

    private bool flightComplete = false;
    public bool FlightComplete
    {
        get { return flightComplete; }
        set
        {
            flightComplete = value;
        }
    }

    public void RestartGame()
    {
        gameTimer = GAME_TIME_LIMIT;
        flightComplete = false;
        engineeringComplete = false;
        securityComplete = false; 
        appState =  APP_STATE.Playing;
        state = GAME_STATE.Navigating; 
    }

    

}
