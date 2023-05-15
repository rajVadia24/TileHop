using UnityEngine;
using UnityEngine.UI;

public class HomeScreenPage : BaseClass
{
    public void OnClick_PlayGameButton()
    {
        ScreenManager.Inst.ShowNextScreen(ScreenType.GamePlayScreen);
    }

    public void OnStartSong_Stronger()
    {
        BallController.Inst.SoundToPlay(SoundNames.Stronger);
    }

    public void OnStartSong_UnderTale()
    {
        BallController.Inst.SoundToPlay(SoundNames.UnderTale);
    }

    public void Play_Stronger()
    {
        AudioManager.Inst.PlaySound(SoundNames.Stronger);
    }

    public void Play_UnderTale()
    {
        AudioManager.Inst.PlaySound(SoundNames.UnderTale);
    }
}
