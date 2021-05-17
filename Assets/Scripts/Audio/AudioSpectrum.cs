using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSpectrum : MonoBehaviour
{
    public static AudioSpectrum I { get; private set; }

    public float spectrumValue;
    public float[] freqBands;

    private readonly int numSamples = 128;
    private readonly int numBands = 6;

    private float[] samples;
    private AudioSource audioSource;

    void Start()
    {
        I = this;

        audioSource = GetComponent<AudioSource>();

        /// initialize buffer
        samples = new float[numSamples];
        freqBands = new float[numBands];
    }

    void Update()
    {
        GetSpectrumAudioSource();
        MakeFrequencyBands();

        // Only takes the first element
        //if (m_audioSpectrum != null && m_audioSpectrum.Length > 0)
        //{
        //    spectrumValue = m_audioSpectrum[0] * 100;
        //}
    }

    void GetSpectrumAudioSource()
    {
        audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
        float max = 0;
        for (int i = 0; i < samples.Length; i++)
        {
            if (samples[i] > max)
            {
                max = samples[i];
            }
        }
        for (int i = 0; i < samples.Length; i++)
        {
            samples[i] /= max;
        }
    }

    void MakeFrequencyBands()
    {
        // fftSize = 256, frequencyBinCount = 128

        int count = 0;
        for (int i = 0; i < numBands; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i);

            for (int j = 0; j < sampleCount; j++)
            {
                average += samples[count];
                count++;
            }

            average /= count;

            freqBands[i] = average;
        }
    }
}
