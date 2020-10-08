using UnityEngine;

public class AudioMaster : MonoBehaviour
{
    public AudioSource audioSource;
    public static float[] samples = new float[512];
    float[] freqBand = new float[8];
    float[] bandBuffer = new float[8];
    float[] bufferDecrease = new float[8];
    float[] freqBandHighest = new float[8];
    public static float[] audioBand = new float[8];
    public static float[] audioBandBuffer = new float[8];

    void Update()
    {
        GetSpectrumAudioSource();
        makeFrequencyBands();
        BandBuffer();
        CreateAudioBands();
    }

    void CreateAudioBands()
    {
        for (int i = 0; i < 8; i++)
        {
            if (freqBand[i] > freqBandHighest[i])
                freqBandHighest[i] = freqBand[i];
            audioBand[i] = freqBand[i] / freqBandHighest[i];
            audioBandBuffer[i] = bandBuffer[i] / freqBandHighest[i];
        }
    }

    void GetSpectrumAudioSource()
    {
        audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
    }

    void BandBuffer()
    {
        for (int i = 0; i < freqBand.Length; i++)
        {
            if (freqBand[i] > bandBuffer[i])
            {
                bandBuffer[i] = freqBand[i];
                bufferDecrease[i] = 0.005f;
            }
            if (freqBand[i] < bandBuffer[i])
            {
                bandBuffer[i] -= bufferDecrease[i];
                bufferDecrease[i] *= 1.2f;
            }
        }
        }

    void makeFrequencyBands()
    {
        int currentSample = 0;
        for (int i = 0; i < freqBand.Length; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;
            if (i == 7)
                sampleCount += 2;
            for (int j = 0; j < sampleCount; j++)
            {
                average += samples[currentSample] * (currentSample + 1);
                currentSample++;
            }
            average /= currentSample;
            freqBand[i] = average * 10;
        }
    }
}
