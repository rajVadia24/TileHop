using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
        //ScreenManager.Inst.ShowNextScreen(ScreenType.HomeScreen);
        _mainMenuButton.transform.DOPunchScale(Vector3.one, 0.15f);
        StartCoroutine(HomeAnimation());
    }

    private void OnClick_RestartButton()
    {
        //ScreenManager.Inst.ShowNextScreen(ScreenType.GamePlayScreen);
        _restartButton.transform.DOPunchScale(Vector3.one, 0.15f);
        StartCoroutine(RestartAnimation());
    }

    private IEnumerator RestartAnimation()
    {
        yield return new WaitForSeconds(0.3f);
        ScreenManager.Inst.ShowNextScreen(ScreenType.GamePlayScreen);
        BallController.Inst.Restart();
    }

    private IEnumerator HomeAnimation()
    {
        yield return new WaitForSeconds(0.3f);
        ScreenManager.Inst.ShowNextScreen(ScreenType.HomeScreen);
        BallController.Inst.Restart();        
    }
}
