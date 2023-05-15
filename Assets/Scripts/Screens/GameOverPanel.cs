using UnityEngine.SceneManagement;
using TMPro;

public class GameOverPanel : BaseClass
{
    public StoreScoreData scoreData;
    public TMP_Text Score;


    private void Start()
    {
        Score.text = "Score: 0"+ scoreData.Score;
    }

    public void OnClick_MainMenuButton()
    {
        ScreenManager.Inst.ShowNextScreen(ScreenType.HomeScreen);
        SceneManager.LoadScene(0);        
    }
}
