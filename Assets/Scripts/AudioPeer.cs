using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPeer : MonoBehaviour
{
    public static float[] samples = new float[512];
    // Normalized
    public static float[] audioBand = new float[8];
    public static float[] audioBandBuffer = new float[8];
    public static float amplitude, amplitudeBuffer;

    private float[] freqBand = new float[8];
    private float[] bandBuffer = new float[8];
    private float[] bufferDecrease = new float[8];

    private float[] freqBandMax = new float[8];
    private float amplitudeMax;

    public AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // GetSpectrumAudioSource();
        MakeFrequencyBands();
        BandBuffer();
        CreateAudioBands();
        GetAmplitude();
    }

    public void SetFrequencySamples(string samples)
    {
        string[] strArr;
        strArr = samples.Split(',');

        for (int i = 0; i < strArr.Length; i++)
        {
            if (i <= samples.Length)
            {
                AudioPeer.samples[i] = float.Parse(strArr[i]) / 255;
            }
        }
    }

    void GetAmplitude()
    {
        float currentAmplitude = 0;
        float currentAmplitudeBuffer = 0;

        for (int i = 0; i < 8; i++)
        {
            currentAmplitude += audioBand[i];
            currentAmplitudeBuffer += audioBandBuffer[i];
        }

        if (currentAmplitude > amplitudeMax)
        {
            amplitudeMax = currentAmplitude;
        }

        amplitude = currentAmplitude / amplitudeMax;
        amplitudeBuffer = currentAmplitudeBuffer / amplitudeMax;
    }

    void CreateAudioBands()
    {
        for (int i = 0; i < 8; i++)
        {
            if (freqBand[i] > freqBandMax[i])
            {
                freqBandMax[i] = freqBand[i];
            }
            audioBand[i] = freqBand[i] / freqBandMax[i];
            audioBandBuffer[i] = bandBuffer[i] / freqBandMax[i];
        }
    }

    // void GetSpectrumAudioSource()
    // {
    //     audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
    // }

    void BandBuffer()
    {
        for (int g = 0; g < 8; ++g)
        {
            if (freqBand[g] > bandBuffer[g])
            {
                bandBuffer[g] = freqBand[g];
                bufferDecrease[g] = 0.005f;
            }
            if (freqBand[g] < bandBuffer[g])
            {
                bandBuffer[g] -= bufferDecrease[g];
                bufferDecrease[g] *= 1.2f;
            }
        }
    }

    void MakeFrequencyBands()
    {
        // If the fftSize is 1024, frequencyBinCount = 512
        // Each frequency bin = 24000/512 = 46.9 Hz
        // samples per bank (if theres 43 hz per sample)
        // 0: 2 samples = 86 hz (0 - 86)
        // 1: 4 = 172 (87 - 258)
        // 2: 8 = 344 (259 - 602)
        // 3: 16 = 688 (603-1290)
        // 4: 32 = 1376
        // ... 7: 256 = 11008 (10923 - 21930)
        // 510 samples

        int count = 0;
        for (int i = 0; i < 8; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;

            for (int j = 0; j < sampleCount; j++)
            {
                average += samples[count];
                count++;
            }

            average /= count;

            freqBand[i] = average;

        }
    }
}
