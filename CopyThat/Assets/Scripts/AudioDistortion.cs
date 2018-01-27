using System.Collections;
using UnityEngine;

/// <summary>
/// Author: Dante Nardo
/// Last Modified: 1/27/2018
/// Purpose: Controls the distortion of the audio being played.
/// </summary>
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioDistortionFilter))]
public class AudioDistortion : MonoBehaviour
{
    #region Audio Distortion Members

    private AudioSource m_audioPlayer;
    private AudioDistortionFilter m_distortionFilter;
    private Distortion m_distortion;

    private AudioClip m_beep;

    #endregion

    #region Audio Distortion Methods

    public void Start()
    {
        m_audioPlayer = GetComponent<AudioSource>();
        m_distortionFilter = GetComponent<AudioDistortionFilter>();
    }

    public void SetDistortion(Distortion distortion)
    {
        m_distortion = distortion;
    }

    public void DistortAudio(AudioClip audio)
    {
        m_audioPlayer.clip = audio;
        StartCoroutine(DistortingAudio());
    }

    private IEnumerator DistortingAudio()
    {
        int i = 0;
        m_audioPlayer.Play();

        // Distort while the index (which is increased at the rate of 1/s) is less than length of audio
        while (i < m_distortion.Length)
        {
            m_distortionFilter.distortionLevel = m_distortion.RadioDistortion[i];
            m_audioPlayer.volume = m_distortion.WaveStrengthDistortion[i];
            yield return new WaitForSeconds(1);
            i++;
        }

        yield break;
    }

    #endregion
}
