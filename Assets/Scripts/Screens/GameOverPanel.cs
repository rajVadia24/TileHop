using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanel : BaseClass
{
    public void OnClick_MainMenuButton()
    {
        ScreenManager.Inst.ShowNextScreen(ScreenType.HomeScreen);
        SceneManager.LoadScene(0);
        DataManager.Inst.AddPlayerScore(ScreenManager.Inst.gamePlayPage._score.text, ScreenManager.Inst.gamePlayPage._highScore.text);
    }
}
