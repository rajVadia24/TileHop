using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeScreenPage : BaseClass
{
    [SerializeField] private Image _background;    

    private void Start()
    {     
        GameStateManager.inst.ChangeState(GameStates.HomeScreen);
    }
}
