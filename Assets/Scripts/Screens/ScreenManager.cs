using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public BaseClass[] Screens;

    public BaseClass CurrentScreen;
    
    public GameOverPanel GameOverObj;

    public static ScreenManager Inst;    

    private void Awake()
    {
        if(Inst != null && Inst != this)
        {
            Destroy(Inst);
            return;
        }
        else
            Inst = this;
    }

    private void Start()
    {
        if(CurrentScreen != null)
            CurrentScreen.canvas.enabled = true;        
    }

    public void ShowNextScreen(ScreenType screenType)
    {
        if (CurrentScreen != null)
            CurrentScreen.canvas.enabled = false;

        foreach (BaseClass baseScreen in Screens)
        {
            if (baseScreen.screenType == screenType)
            {
                baseScreen.canvas.enabled = true;
                CurrentScreen = baseScreen;
                break;
            }

        }

        switch (screenType)
        {
            case ScreenType.HomeScreen:

                GameStateManager.inst.ChangeState(GameStates.HomeScreen);                
                //BallController.Inst.enabled = false;
                DataManager.Inst.DisplayNewData();
                DataManager.Inst.SaveJsonData();
                break;

            case ScreenType.GamePlayScreen:

                GameStateManager.inst.ChangeState(GameStates.GamePlay);                
                AudioManager.Inst.StopSound();
                //BallController.Inst.enabled = true;
                break;

            case ScreenType.GameOverPanel:

                GameStateManager.inst.ChangeState(GameStates.GameOver);
                break;
        }
    }
}