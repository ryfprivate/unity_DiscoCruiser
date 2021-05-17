using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSpectrum : MonoBehaviour
{
    public static float spectrumValue { get; private set; }

    private float[] m_audioSpectrum;

    void Start()
    {
        /// initialize buffer
        m_audioSpectrum = new float[128];
    }

    void Update()
    {
        AudioListener.GetSpectrumData(m_audioSpectrum, 0, FFTWindow.Hamming);

        // Only takes the first element
        if (m_audioSpectrum != null && m_audioSpectrum.Length > 0)
        {
            spectrumValue = m_audioSpectrum[0] * 100;
        }
    }
}
