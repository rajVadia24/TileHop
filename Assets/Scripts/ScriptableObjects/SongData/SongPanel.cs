using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SongPanel : MonoBehaviour
{
    public TMP_Text SongName;
    public Image SongImage;
    public TMP_Text HighScore;    
    [SerializeField] private Button _playGameButton;

    private void Start()
    {
        _playGameButton.onClick.AddListener(OnClick_PlayGameButton);        
    }

    private void OnClick_PlayGameButton()
    {
        DataManager.Inst.CurrentSong(SongName.text);
        DataManager.Inst.AddDataFromSO();        
        ScreenManager.Inst.ShowNextScreen(ScreenType.GamePlayScreen);
    }
}
