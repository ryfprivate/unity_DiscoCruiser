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

    void Update()
    {
        // string test = "164,178,190,184,163,146,129,115,101,96,92,90,111,126,114,85,78,83,72,86,89,67,53,54,78,92,92,77,59,60,66,67,75,82,68,61,60,58,57,75,82,75,77,71,73,77,69,55,44,61,66,54,46,53,48,34,40,47,57,62,53,39,42,48,52,65,68,63,60,62,56,53,46,50,63,62,57,60,63,61,55,46,47,50,53,65,64,60,56,59,56,52,49,49,47,42,41,47,51,50,48,49,52,51,42,41,48,52,60,61,55,45,33,41,53,54,50,46,40,38,41,40,35,27,31,41,43,33,34,33,30,32,44,51,47,46,42,38,29,27,27,29,33,37,38,33,33,37,34,25,17,22,24,20,30,36,37,28,23,23,22,28,28,27,36,34,31,32,25,24,20,21,22,19,22,27,14,24,36,40,43,34,19,29,27,28,32,35,26,25,24,22,22,23,25,17,15,16,20,16,17,15,16,20,20,16,19,24,23,16,11,11,4,2,13,17,11,17,20,15,9,7,7,6,8,10,10,1,5,14,20,22,14,0,8,14,9,10,15,11,5,6,5,0,0,6,12,19,18,0,1,6,12,6,0,0,0,5,9,12,14,6,3,8,2,0,0,0,0,8,13,5,7,12,5,0,4,7,0,0,0,0,3,4,3,0,2,9,4,4,4,1,0,2,5,0,0,0,0,0,0,0,3,7,6,2,0,0,0,0,0,3,7,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,7,10,3,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,2,0,1,6,5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0";
        // SetFrequencySamples(test);
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
