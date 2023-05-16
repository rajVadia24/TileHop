using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SongPanel : MonoBehaviour
{
    public TMP_Text _songName;
    public Image _songImage;
    [SerializeField] private TMP_Text _score;
    [SerializeField] private Button _playGameButton;

    private void Start()
    {
        _playGameButton.onClick.AddListener(OnClick_PlayGameButton);        
    }

    private void OnClick_PlayGameButton()
    {
        DataManager.Inst.CurrentSong(_songName.text);
        ScreenManager.Inst.ShowNextScreen(ScreenType.GamePlayScreen);
    }
}
