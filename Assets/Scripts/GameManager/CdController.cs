using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CdController : MonoBehaviour
{
    public static CdController inst;

    [SerializeField] private Image _songImage;
    [SerializeField] private Button _imageButton;
    [SerializeField] private AudioSource _audio;

    private float yolo;
    private AudioTrack _audioTrack;

    public static Action OnRotate;

    private void Awake()
    {
        inst = this;
    }

    private void OnEnable()
    {
        GameStateManager.OnGameStateChange += ChangeState;
    }

    private void OnDisable()
    {
        OnRotate = null;
        GameStateManager.OnGameStateChange -= ChangeState;
    }

    public void ChangeState(GameStates gs)
    {
        switch (gs)
        {
            case GameStates.GamePlay:
                OnRotate = null;
                ResetRotation();
                break;
        }
    }

    private void Start()
    {
        _imageButton.onClick.AddListener(OnClick_Image);
        transform.DOScale(new Vector3(1, 1, 1), 1f).SetEase(Ease.OutFlash);
    }

    private void Update()
    {
        OnRotate?.Invoke();
    }

    private void RotateCD()
    {
        transform.RotateAround(transform.position, -transform.forward, Time.deltaTime * 90f);
    }

    public void ResetRotation()
    {
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    private void OnClick_Image()
    {
        if (!_audio.isPlaying)
        {
            OnRotate += RotateCD;
            AudioManager.Inst.PlaySound(_audioTrack);
        }
        else
        {
            OnRotate = null;
            _audio.Stop();
        }
    }

    public void AudioToPlay(AudioTrack audio, Sprite songImage)
    {
        _audio.Stop();
        _audioTrack = audio;
        _songImage.sprite = songImage;
        Debug.Log("SONGIMAGE==> " + songImage);
    }
}
