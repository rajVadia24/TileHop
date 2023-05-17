using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseClass : MonoBehaviour
{
    public static BaseClass Inst;

    [HideInInspector]
    public Canvas canvas;

    public ScreenType screenType;

    private void Awake()
    {
        Inst = this;
            
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
    }
}

public enum ScreenType
{
    HomeScreen,
    GamePlayScreen,
    GameOverPanel,
}