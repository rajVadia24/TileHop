using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Inst;

    private AudioSource audioSource;

    [SerializeField] Sound[] sound;

    private void Awake()
    {
        Inst = this;                
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //PlaySound(SoundNames.Theme);
    }

    public void PlaySound(SoundNames name)
    {
        if (audioSource.isPlaying) { audioSource.Stop(); }

        foreach (Sound sounds in sound)
        {
            if (sounds.name == name)
                audioSource.PlayOneShot(sounds.clip);
        }
    }

    public void StopSound()
    {
        audioSource.Stop();
    }


    [System.Serializable]
    public class Sound
    {
        public SoundNames name;
        public AudioClip clip;
    }
}

public enum SoundNames
{
    Stronger,
    UnderTale,
    Closer,
}