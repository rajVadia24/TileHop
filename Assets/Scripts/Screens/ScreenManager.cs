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

        if(screenType == ScreenType.GamePlayScreen)
        {
            AudioManager.Inst.StopSound();
            BallController.Inst.enabled = true;
        }
        else if(screenType == ScreenType.HomeScreen)
        {
            BallController.Inst.enabled = false;
            DataManager.Inst.DisplayNewData();            
            DataManager.Inst.SaveJsonData();            
        }
    }
}