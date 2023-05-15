using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SongPanel : MonoBehaviour
{
    [SerializeField] private AddSong song;

    [SerializeField] private TMP_Text _songName;
    [SerializeField] private Image _songImage;    

    private void Start()
    {
        _songName.text = song.SongName;
        _songImage.sprite = song.SongImage;
        DataManager.Inst.AddSongData(_songName.text);
    }

    public void GetSongName()
    {
        DataManager.Inst.CurrentSong(_songName.text);        
    }
}
