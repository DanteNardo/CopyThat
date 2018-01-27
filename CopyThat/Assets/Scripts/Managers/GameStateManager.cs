using UnityEngine;

public enum GAME_STATE
{
    Navigating, 
    Communication, 
    Engineering, 
    Security, 
    Flight
}

public class GameStateManager : Singleton<GameStateManager>
{
    protected GameStateManager () { } // // guarantee this will be always a singleton only - can't use the constructor!

    // Global vars here 
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

}
