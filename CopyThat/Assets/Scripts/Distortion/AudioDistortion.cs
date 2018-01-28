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

    public AudioSource m_audioPlayer;
    public AudioSource m_staticPlayer;
    //public AudioSource m_beepPlayer;
    private AudioDistortionFilter m_distortionFilter;
    private Distortion m_distortion;

    public bool Distorting { get; set; }

    private const float MinStatic = 0.0f;
    private const float MaxStatic = 0.5f;

    #endregion

    #region Audio Distortion Methods

    public void Start()
    {
        m_distortionFilter = GetComponent<AudioDistortionFilter>();
        m_distortionFilter.distortionLevel = 0;
        Distorting = false;
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
        float t = 0.0f;
        float staticVolume = m_staticPlayer.volume;
        float audioVolume = m_audioPlayer.volume;
        float nextStatic = 1 - m_distortion.WaveStrengthDistortion[i];
        float nextAudio = m_distortion.WaveStrengthDistortion[i];
        m_staticPlayer.Play();
        m_audioPlayer.Play();
        Distorting = true;

        // Distort while the index (which is increased at the rate of 1/s) is less than length of audio
        while (i < m_distortion.Length)
        {
            t += Time.deltaTime;

            m_staticPlayer.volume = Mathf.Lerp(staticVolume, nextStatic, t);
            m_audioPlayer.volume = Mathf.Lerp(audioVolume, nextAudio, t);

            if (t > 1.0f)
            {
                staticVolume = nextStatic;
                audioVolume = nextAudio;
                nextStatic = Mathf.Clamp(1 - m_distortion.WaveStrengthDistortion[i], MinStatic, MaxStatic);
                nextAudio = m_distortion.WaveStrengthDistortion[i];

                t = 0.0f;
                i++;
            }

            yield return null;
        }

        m_staticPlayer.volume = 0.5f;
        m_audioPlayer.volume = 1.0f;
        m_staticPlayer.Stop();
        m_audioPlayer.Stop();
        Distorting = false;

        yield break;
    }

    #endregion
}
