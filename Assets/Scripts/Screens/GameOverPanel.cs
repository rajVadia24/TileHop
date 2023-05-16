using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : BaseClass
{    
    [SerializeField] private TMP_Text _score;
    [SerializeField] private TMP_Text _highScore;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _mainMenuButton;

    private void Start()
    {
        _restartButton.onClick.AddListener(OnClick_RestartButton);
        _mainMenuButton.onClick.AddListener(OnClick_MainMenuButton);
    }

    public void DisplayScore()
    {
        _score.text = "Score: 0" + ScoreManager.Inst.Score;
        _highScore.text = "HighScore: 0" + ScoreManager.Inst.HighScore;
    }

    public void OnClick_MainMenuButton()
    {
        DataManager.Inst.AddPlayerScore(ScoreManager.Inst.Score.ToString(), ScoreManager.Inst.HighScore.ToString());
        ScreenManager.Inst.ShowNextScreen(ScreenType.HomeScreen);                
    }

    private void OnClick_RestartButton()
    {
        ScreenManager.Inst.ShowNextScreen(ScreenType.GamePlayScreen);
        BallController.Inst.Restart();
    }
}
