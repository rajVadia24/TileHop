using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Inst;

    private float _songLength;
    
    [SerializeField] private float[] _speedThresholds = { 0.1f, 0.3f, 0.5f, 0.7f, 0.9f };
    [SerializeField] private float[] _speedValues = { 2f, 2.3f, 2.7f, 3.5f, 4f };     
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
        //Debug.Log("AudioTime==> " + audioSource.time);
        IncreaseSpeedWithSong();        
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

            for (int i = 0; i < _speedThresholds.Length; i++)
            {
                if (songProgress >= _speedThresholds[i])
                {
                    BallController.Inst.ConstantSpeed = _speedValues[i];                    
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
    ShapeOfYou,
    UnderTale,
    Closer,
    CallmeMaybe,
    BallHitSound,
}