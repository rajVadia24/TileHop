using System.Collections.Generic;
using UnityEngine;

public class BeatDetector : MonoBehaviour
{
    public static BeatDetector Inst;

    public AudioSource audioSource;

    public float[] _beatTimes;
    public float beatThreshold = 0.2f;
    public float beatInterval = 0.3f;    

    private float lastBeatTime;

    private void Awake()
    {
        Inst = this;
    }

    void Start()
    {
        audioSource.Play();
        lastBeatTime = -beatInterval;
    }

    void Update()
    {
        for(int i = 0; i< _beatTimes.Length; i++)
        {            
            Debug.Log(_beatTimes[i]);
        }        

        if (audioSource.time < lastBeatTime + beatInterval)
        {
            return;
        }

        float[] spectrum = new float[1024];
        audioSource.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);

        float sum = 0f;
        for (int i = 0; i < spectrum.Length; i++)
        {
            sum += spectrum[i];
        }
        float average = sum / spectrum.Length;

        if (average >= beatThreshold)
        {
            lastBeatTime = audioSource.time;
            AddBeatTime(lastBeatTime);
        }
    }

    void AddBeatTime(float beatTime)
    {
        List<float> temp = new List<float>(_beatTimes);
        temp.Add(beatTime);
        _beatTimes = temp.ToArray();        
    }    
}
