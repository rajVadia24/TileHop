using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    //public static DataManager Inst;

    public PlayerData playerData;    

    private static string Path;

    private string _currentSong;

    private void Awake()
    {
        //Inst = this;
        Path = Application.persistentDataPath + "TilesHop.json";
    }

    private void Start()
    {
        LoadJsonData();
    }

    public void AddSongData(string songName)
    {
        SongData songData = new();
        songData.SongName = songName;
        songData.scoreData = new();

        playerData.songData.Add(songData);
    }

    public void AddPlayerScore(string score, string highScore)
    {        
        SongData currentSong = playerData.songData.Find(u => u.SongName == _currentSong);

        if(currentSong != null)
        {
            currentSong.scoreData.Score = score;
            currentSong.scoreData.HighScore = highScore;
        }
    }

    public void CurrentSong(string songName)
    {
        _currentSong = songName;
    }

    public void SaveJsonData()
    {
        string path = Path;
        File.WriteAllText(path, JsonUtility.ToJson(playerData));
    }

    private void LoadJsonData()
    {
        string path = Path;

        if (File.Exists(Path))
        {
            string json = File.ReadAllText(Path);
            playerData = JsonUtility.FromJson<PlayerData>(json);

            Debug.Log("Json: " + json);
        }
        else
            Debug.LogError("File Doesn't Exist");
    }
}

[System.Serializable]
public class PlayerData
{
    public List<SongData> songData = new();
}

[System.Serializable]
public class SongData
{
    public string SongName;
    public ScoreData scoreData = new();
}

[System.Serializable]
public class ScoreData
{
    public string Score;
    public string HighScore;
}
