using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;
using System;

public class SongPanel : MonoBehaviour
{
    public TMP_Text SongName;
    public Image SongImage;
    public TMP_Text HighScore;    
    [SerializeField] private Button _playGameButton;
    [SerializeField] private Button _playSongButton;    

    private void Start()
    {
        _playGameButton.onClick.AddListener(OnClick_PlayGameButton);
        _playSongButton.onClick.AddListener(OnClick_PlaySongButton);
    }

    private void OnClick_PlaySongButton()
    {
        CdController.OnRotate = null;
        CdController.inst.ResetRotation();
        DataManager.Inst.SongToBePlayed(SongName.text);
    }

    private void OnClick_PlayGameButton()
    {
        DataManager.Inst.CurrentSong(SongName.text);        
        _playGameButton.transform.DOPunchScale(Vector3.one, 0.15f);
        
        StartCoroutine(PlayAnimation());
    }

    private IEnumerator PlayAnimation()
    {
        yield return new WaitForSeconds(0.3f);
        ScreenManager.Inst.ShowNextScreen(ScreenType.GamePlayScreen);
    }
}
