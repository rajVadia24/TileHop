using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "AddSong", menuName = "SongName")]
[Serializable]
public class AddSongData : ScriptableObject
{
    public List<SongData> songData = new();
}

[Serializable]
public class SongData
{
    public string SongName;
    public int HighScore;
    public Sprite SongImage;
    public AudioTrack audioTrack;
}
