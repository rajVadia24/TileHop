using UnityEngine;
using UnityEngine.UI;

public class HomeScreenPage : BaseClass
{
    private void Start()
    {
        GameStateManager.inst.ChangeState(GameStates.HomeScreen);
    }
}
