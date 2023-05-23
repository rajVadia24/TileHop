using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CdController : MonoBehaviour
{
    [SerializeField] private Button _imageButton;
    [SerializeField] private AudioSource _audio;

    private bool isPlaying;

    private void Start()
    {
        _imageButton.onClick.AddListener(OnClick_Image);
    }

    private void Update()
    {
        if (isPlaying)
        {
            transform.RotateAround(transform.position, -transform.forward, Time.deltaTime * 90f);
        }
    }

    private void OnClick_Image()
    {
        if (!_audio.isPlaying)
        {
            isPlaying = true;
            AudioManager.Inst.PlaySound(AudioTrack.ShapeOfYou);
        }
        else
        {
            isPlaying = false;
            _audio.Stop();
        }

    }
}
