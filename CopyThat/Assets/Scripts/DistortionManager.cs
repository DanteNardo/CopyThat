using UnityEngine;

/// <summary>
/// Author: Dante Nardo
/// Last Modified: 1/27/2018
/// Purpose: A Singleton that handles all of the game's audio and text distortion.
/// </summary>
[RequireComponent(typeof(AudioDistortion))]
[RequireComponent(typeof(TextDistortion))]
public class DistortionManager : MonoBehaviour
{
    #region Distortion Manager Members

    public static DistortionManager Instance;
    private Distortion m_distortion;

    private AudioDistortion m_audioDistorter;
    private TextDistortion m_textDistorter;
    public AudioDistortion AudioDistorter { get { return m_audioDistorter; } }
    public TextDistortion TextDistorter { get { return m_textDistorter; } }

    public AudioClip m_currentAudio;
    private string m_currentText;

    #endregion

    #region Distortion Manager Methods

    // Create a singleton
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        m_distortion = new Distortion();
        m_audioDistorter = GetComponent<AudioDistortion>();
        m_textDistorter = GetComponent<TextDistortion>();
        m_audioDistorter.SetDistortion(m_distortion);
        m_textDistorter.SetDistortion(m_distortion);
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            DistortTransmission(m_currentAudio, "text");
        }
    }

    public void DistortTransmission(AudioClip audio, string text)
    {
        m_distortion.CalculateDistortion((int)audio.length);
        DistortAudio(audio);
        DistortText(text);
    }

    private void DistortAudio(AudioClip audio)
    {
        m_currentAudio = audio;
        m_audioDistorter.DistortAudio(audio);
    }

    private void DistortText(string text)
    {
        m_currentText = text;
        //m_textDistorter.DistortText(text);
    }

    public string GetText()
    {
        return m_textDistorter.GetText();
    }

    #endregion
}
