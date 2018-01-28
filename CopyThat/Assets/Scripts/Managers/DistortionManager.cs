using UnityEngine;

/// <summary>
/// Author: Dante Nardo
/// Last Modified: 1/27/2018
/// Purpose: A Singleton that handles all of the game's audio and text distortion.
/// </summary>
[RequireComponent(typeof(AudioDistortion))]
public class DistortionManager : MonoBehaviour
{
    #region Distortion Manager Members

    public static DistortionManager Instance;
    private Distortion m_distortion;

    private AudioDistortion m_audioDistorter;
    public AudioDistortion AudioDistorter { get { return m_audioDistorter; } }
    public AudioClip m_currentAudio;

    private float randMod1 = 1;
    private float randMod2 = 1;
    private float randMod3 = 1;

    #endregion

    #region Distortion Manager Methods
    
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
        m_audioDistorter.SetDistortion(m_distortion);
    }

    private void Start()
    {
        randMod1 = Random.Range(-2.0f, 2.0f);
        randMod2 = Random.Range(-2.0f, 2.0f);
        randMod3 = Random.Range(-2.0f, 2.0f);
    }

    public void DistortTransmission(AudioClip audio)
    {
        m_distortion.CalculateDistortion((int)audio.length + 1);
        DistortAudio(audio);
    }

    private void DistortAudio(AudioClip audio)
    {
        m_currentAudio = audio;
        m_audioDistorter.DistortAudio(audio);
    }

    public void SendDistortionMods(float mod1, float mod2, float mod3)
    {
        m_distortion.SetMod(mod1 * randMod1  + mod2 * randMod2 + mod3 * randMod3);
    }

    #endregion
}
