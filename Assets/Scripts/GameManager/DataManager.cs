using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    public static DataManager Inst;    

    [SerializeField] private GameObject _parentContent;
    [SerializeField] private GameObject _songPanel;

    public List<AddSongData> SongData_So;

    public PlayerData playerData;

    private static string Path;

    private string _currentSong;


    private void Awake()
    {
        //if (Inst == null)
        //{
            Inst = this;
        //    DontDestroyOnLoad(this);
        //}
        //else
        //    Destroy(gameObject);

        Path = Application.persistentDataPath + "TilesHop.json";
    }

    private void Start()
    {
        LoadJsonData();
        AddDataFromSO();
        DisplaySongPanel();
    }

    private void AddDataFromSO()
    {
        for(int i=0; i<SongData_So.Count; i++)
        {
            AddSongData(SongData_So[i].SongName, SongData_So[i].SongImage);
        }
    }

    public void AddSongData(string songName, Sprite songImage)
    {
        SongData songData = new();
        songData.SongName = songName;
        songData.SongImage = songImage;
        songData.scoreData = new();

        playerData.songData.Add(songData);
    }

    public void AddPlayerScore(string score, string highScore)
    {        
        SongData currentSong = playerData.songData.Find(u => u.SongName == _currentSong);
        Debug.Log("Current: " + currentSong.SongName);
        if(currentSong != null)
        {
            currentSong.scoreData.Score = score;
            currentSong.scoreData.HighScore = highScore;
        }
    }

    public void CurrentSong(string songName)
    {
        _currentSong = songName;
        Debug.Log(_currentSong);
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

    public void DisplaySongPanel()
    {
        for(int i =0; i<playerData.songData.Count; i++)
        {
            GameObject songPanelClone = Instantiate(_songPanel, _parentContent.transform);
            SongPanel displaySongPanel = songPanelClone.GetComponent<SongPanel>();
            displaySongPanel._songName.text = playerData.songData[i].SongName;
            displaySongPanel._songImage.sprite = playerData.songData[i].SongImage;
        }
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
    public Sprite SongImage;
    public ScoreData scoreData = new();
}

[System.Serializable]
public class ScoreData
{
    public string Score;
    public string HighScore;
}
