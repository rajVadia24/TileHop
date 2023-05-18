using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Inst;

    private float _songLength;

    [SerializeField] private float[] speedThresholds = { 0.1f, 0.3f, 0.5f, 0.7f, 0.9f };
    [SerializeField] private float[] speedValues = { 2f, 2.3f, 2.7f, 3.5f, 4f };
    private float _startTime;

    [SerializeField] private AudioSource audioSource;

    public Sound[] sound;

    //public Action OnStopTileSpawn;

    private void Awake()
    {
        Inst = this;                
    }        

    private void Update()
    {
        Debug.Log("AudioTime==> " + audioSource.time);
        IncreaseSpeedWithSong();
        //OnStopTileSpawn.Invoke();
    }

    public void PlaySound(AudioTrack name)
    {
        _startTime = Time.time;

        if (audioSource.isPlaying) { audioSource.Stop(); }

        foreach (Sound sounds in sound)
        {
            if (sounds.name == name)
            {
                audioSource.PlayOneShot(sounds.clip);
            }            
        }
    }

    public void StopSound()
    {        
        audioSource.Stop();        
    }

    //public void StopTileSpawning()
    //{
    //    if (audioSource.isPlaying == false)
    //    {
    //        Debug.LogWarning("SONG End!");
    //        SpawnManager.Inst.OnSpawnTile = null;
    //        OnStopTileSpawn = null;
    //    }
    //    else if (audioSource.isPlaying == true)
    //    {
    //        Debug.LogWarning("SONG Started!");
    //        SpawnManager.Inst.OnSpawnTile += SpawnManager.Inst.SpawnTile;            
    //    }
    //}

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
                    //Debug.Log("Score is " + (speedThresholds[i] * 100) + "%");
                }
                else
                {
                    break;
                }                

                //Debug.Log("PROGRESS ==> " + songProgress);
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
    ShapeOfYou,
    UnderTale,
    Closer,
    CallmeMaybe,
    BallHitSound,
}