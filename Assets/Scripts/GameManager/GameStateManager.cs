using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager inst;
    public static Action<GameStates> OnGameStateChange;

    public GameStates CurrentState;

    private void Awake()
    {
        inst = this;
    }

    public void ChangeState(GameStates gs)
    {
        CurrentState = gs;
        OnGameStateChange?.Invoke(gs);
    }

}

public enum GameStates
{
    HomeScreen,
    GamePlay,
    GameOver,
}
