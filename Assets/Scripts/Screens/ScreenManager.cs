using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public BaseClass[] Screens;

    public BaseClass CurrentScreen;

    public GamePlayPage gamePlayPage;

    public static ScreenManager Inst;

    private void Awake()
    {
        Inst = this;
    }

    private void Start()
    {
        CurrentScreen.canvas.enabled = true;
    }

    public void ShowNextScreen(ScreenType screenType)
    {
        CurrentScreen.canvas.enabled = false;

        foreach (BaseClass baseScreen in Screens)
        {
            if (baseScreen.screenType == screenType)
            {
                baseScreen.canvas.enabled = true;
                break;
            }

            CurrentScreen = baseScreen;
        }

        if(screenType == ScreenType.GamePlayScreen)
        {
            AudioManager.Inst.StopSound();
            BallController.Inst.enabled = true;
        }
    }
}