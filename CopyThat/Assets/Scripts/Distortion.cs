using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Dante Nardo
/// Last Modified: 1/26/2018
/// Purpose: Creates different distortion values for different types of sounds.
/// </summary>
public class Distortion
{
    #region Distortion Members

    private float m_length;
    public float m_beepIter = 1.0f;
    public float m_waveIter = 1.0f;
    private List<float> m_radioDistortion;          // Used to determine the strength of radio static
    private List<float> m_beepDistortion;           // Used to determine when to beep and for how long
    private List<float> m_waveStrengthDistortion;   // Used to determine the overall strength of the signal

    // Properties for getting
    public float Length { get { return m_length; } }
    public List<float> RadioDistortion { get { return m_radioDistortion; } }
    public List<float> BeepDistortion { get { return m_beepDistortion; } }
    public List<float> WaveStrengthDistortion { get { return m_waveStrengthDistortion; } }

    #endregion

    #region Distortion Methods

    /// <summary>
    /// Calculates each form of distortion for audio and text.
    /// </summary>
    /// <param name="length">The length of the distortion data</param>
    public void CalculateDistortion(int length)
    {
        m_length = length;

        m_radioDistortion = new List<float>();
        m_beepDistortion = new List<float>();
        m_waveStrengthDistortion = new List<float>();

        CreateRadioDistortion();
        CreateBeepDistortion();
        CreateWaveStrengthDistortion();
    }

    /// <summary>
    /// Use Perlin Noise to determine the overall strength of radio static.
    /// </summary>
    public void CreateRadioDistortion()
    {
        // Decide a random spot in the PerlinNoise tex and determine iter
        float xPos = Random.Range(0.0f, 1.0f);
        float yPos = Random.Range(0.0f, 1.0f);
        float iter = m_length * 0.01f;

        // Iterate through Perlin Noise and assign values
        for (int i = 0; i < m_length; i++)
        {
            // Update pos in Perlin Noise each iteration
            xPos += iter;
            if (xPos > 1.0f)
            {
                xPos = 0.0f;
                yPos += iter;
                if (yPos > 1.0f)
                    yPos = 0.0f;
            }

            // Add Perlin Noise value to radio distortion list
            m_radioDistortion.Add(Mathf.PerlinNoise(xPos, yPos));
        }
    }

    /// <summary>
    /// Randomly create beeps and glitches of varying strength and length.
    /// </summary>
    public void CreateBeepDistortion()
    {
        float r = Random.Range(0, 101);
        float iter = m_beepIter;

        for (int i = 0; i < m_length; i++)
        {
            // Beep generated
            if (r >= 100)
            {
                // Register beep and reset chance to beep to 1/100
                m_beepDistortion.Add(1.0f);
                r = Random.Range(0, 101);
                iter = 0;
            }
            // No beep generated, increase chance of beeping
            else
            {
                m_beepDistortion.Add(0.0f);

                // Determine random value and increase iter (chance of beep)
                r = Random.Range(0, 101);
                r += iter;
            }

        }
    }

    /// <summary>
    /// Uses a sin wave to determine the overall strength of the transmission over time.
    /// </summary>
    public void CreateWaveStrengthDistortion()
    {
        // Random starting sin function position
        float pos = Random.Range(0.0f, 2 * Mathf.PI);
        for (int i = 0; i < m_length; i++)
        {
            m_waveStrengthDistortion.Add(Mathf.Clamp(Mathf.Sin(pos), 0.2f, 1.0f));
            pos += m_waveIter;
        }
    }

    #endregion
}
