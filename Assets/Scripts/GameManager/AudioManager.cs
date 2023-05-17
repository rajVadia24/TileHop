using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Inst;

    private float _songLength;

    [SerializeField] private float[] speedThresholds = { 0.1f, 0.3f, 0.5f, 0.7f, 0.9f };
    [SerializeField] private float[] speedValues = { 1.8f, 2.1f, 2.5f, 2.9f, 3.5f };
    private float _startTime;

    [SerializeField] private AudioSource audioSource;

    public Sound[] sound;

    private void Awake()
    {
        Inst = this;                
    }    

    private void Update()
    {
        Debug.Log("AudioTime==> " + audioSource.time);
        IncreaseSpeedWithSong();
    }

    public void PlaySound(AudioTrack name)
    {
        if (audioSource.isPlaying) { audioSource.Stop(); }

        foreach (Sound sounds in sound)
        {
            if (sounds.name == name)
            {
                audioSource.PlayOneShot(sounds.clip);
                _startTime = Time.time;
            }            
        }
    }

    public void StopSound()
    {        
        audioSource.Stop();        
    }

    public void LengthOfAudio(AudioTrack currentSong)
    {
        float length = sound.Length;
        for (int i = 0; i < length; i++)
        {
            if (sound[i].name == currentSong)
            {
                _songLength = sound[i].clip.length;
            }
        }
    }

    public void IncreaseSpeedWithSong()
    {
        if (_songLength > 0f)
        {
            float songProgress = (Time.time - _startTime) / _songLength;

            for (int i = 0; i < speedThresholds.Length; i++)
            {
                if (songProgress >= speedThresholds[i])
                {
                    BallController.Inst.ConstantSpeed = speedValues[i];
                    Debug.Log("Score is " + (speedThresholds[i] * 100) + "%");
                }
                else
                {
                    break;
                }
            }
        }
    }

    [System.Serializable]
    public class Sound
    {
        public AudioTrack name;
        public AudioClip clip;
    }
}

public enum AudioTrack
{
    Stronger,
    UnderTale,
    Closer,
}