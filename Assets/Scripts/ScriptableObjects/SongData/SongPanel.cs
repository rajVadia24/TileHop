using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SongPanel : MonoBehaviour
{
    [SerializeField] private AddSongData song;

    [SerializeField] private TMP_Text _songName;
    [SerializeField] private Image _songImage;    

    private void Start()
    {
        _songName.text = song.SongName;
        _songImage.sprite = song.SongImage;       
    }    
}
