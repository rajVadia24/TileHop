using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "AddSong", menuName = "SongName")]
public class AddSongData : ScriptableObject
{
    public string SongName;
    public int Score;
    public Sprite SongImage;
}
