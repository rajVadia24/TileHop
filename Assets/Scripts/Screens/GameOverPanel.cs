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
        _score.text = "Score: " + ScoreManager.Inst.Score;
        _highScore.text = "HighScore: " + ScoreManager.Inst.HighScore;
    }

    public void OnClick_MainMenuButton()
    {
        DataManager.Inst.AddPlayerScore(ScoreManager.Inst.HighScore);
        ScreenManager.Inst.ShowNextScreen(ScreenType.HomeScreen);
        BallController.Inst.Restart();        
    }

    private void OnClick_RestartButton()
    {
        ScreenManager.Inst.ShowNextScreen(ScreenType.GamePlayScreen);
        BallController.Inst.Restart();
    }
}
