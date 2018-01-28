using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Dante Nardo
/// Last Modified: 1/27/2018
/// Purpose: Controls the playback and strength of transmission.
/// </summary>
public class CommsPanel : MonoBehaviour
{
    #region Comms Panel Members

    public RawSliderBehavior m_slider1;
    public RawSliderBehavior m_slider2;
    public RawSliderBehavior m_slider3;
    public SwitchBehavior m_button;

    public List<AudioClip> m_instructions;
    private int m_currentInstruction = 0;

    #endregion

    #region Comms Panel Methods

    public void SendTransmission()
    {
        if (DistortionManager.Instance.AudioDistorter.Distorting == false)
        {
            DistortionManager.Instance.SendDistortionMods(
                m_slider1.sliderReturn,
                m_slider2.sliderReturn,
                m_slider3.sliderReturn);
            DistortionManager.Instance.DistortTransmission(m_instructions[m_currentInstruction]);
        }
    }

    #endregion
}
