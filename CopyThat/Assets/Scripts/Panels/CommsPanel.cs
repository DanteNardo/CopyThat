using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

/// <summary>
/// Author: Dante Nardo
/// Last Modified: 1/27/2018
/// Purpose: Controls the playback and strength of transmission.
/// </summary>
public class CommsPanel : MonoBehaviour
{
    #region Comms Panel Members

    private Slider[] sliders; 

    public List<AudioClip> m_instructions;
    private int m_currentInstruction = 0;

    #endregion

    #region Comms Panel Methods

    private void Start()
    {
        sliders = GetComponentsInChildren<Slider>(); 
    }

    public void SendTransmission()
    {
        if (DistortionManager.Instance.AudioDistorter.Distorting == false)
        {
            DistortionManager.Instance.SendDistortionMods(
                sliders[0].value,
                sliders[1].value,
                sliders[2].value);
            DistortionManager.Instance.DistortTransmission(m_instructions[m_currentInstruction]);
            SetNewTarget();
        }
    }

    public void NextInstruction()
    {
        m_currentInstruction++;
        SetNewTarget();
    }

    private void SetNewTarget()
    {
        switch (m_currentInstruction)
        {
            case 0:
                GameStateManager.Instance.TargetState = GAME_STATE.Security;
                break;
            case 1:
                GameStateManager.Instance.TargetState = GAME_STATE.Engineering;
                break;
            case 2:
                GameStateManager.Instance.TargetState = GAME_STATE.Flight;
                break;
            case 3:
                GameStateManager.Instance.AppState = APP_STATE.Won;
                break;
            default:
                Debug.Log("This should never happen.");
                break;
        }
    }

    #endregion
}
