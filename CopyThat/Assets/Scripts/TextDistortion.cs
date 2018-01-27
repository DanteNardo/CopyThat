using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDistortion : MonoBehaviour
{
    private Distortion m_distortion;
    private string m_text;

    public string GetText()
    {
        return m_text;
    }

    public void SetDistortion(Distortion distortion)
    {
        m_distortion = distortion;
    }

    //private IEnumerator DistortingText()
    //{

    //}
}
