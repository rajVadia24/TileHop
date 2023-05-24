using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseClass : MonoBehaviour
{    
    [HideInInspector]
    public Canvas canvas;

    public ScreenType screenType;

    private void Awake()
    {                    
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
    }

    private void Start()
    {
        Debug.LogError("YOLO");
    }
}

public enum ScreenType
{
    HomeScreen,
    GamePlayScreen,
    GameOverPanel,
    Settings,
}