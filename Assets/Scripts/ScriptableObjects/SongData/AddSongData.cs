using UnityEngine;

[CreateAssetMenu(fileName = "AddSong", menuName = "SongName")]
public class AddSongData : ScriptableObject
{
    public string SongName;
    public int Points;
    public Sprite SongImage;
}
