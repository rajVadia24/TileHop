using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Inst;    

    [SerializeField] private GameObject _parentContent;
    [SerializeField] private GameObject _songPanel;

    public List<AddSongData> So_SongData;    
    private List<GameObject> _clonedPrefabs = new();

    public GameData gameData;

    private static string Path;

    private string _currentSong;    

    private void Awake()
    {        
        Inst = this;
        Path = Application.persistentDataPath + "TileHop.json";
    }

    private void Start()
    {        
        LoadJsonData();
        LoadDataToSO();
        AddDataFromSO();
        DisplaySongPanel();
    }
    
    private void OnApplicationQuit()
    {
        AddSongDataToJson();
    
    }

    public void AddDataFromSO()
    {        
        AddSongData currentSong = So_SongData.Find(u => u.SongName == _currentSong);
        if (currentSong != null)
        {
            ScoreManager.Inst.HighScore = currentSong.HighScore;
            BallController.Inst.SoundToPlay(currentSong.audioTrack);
            AudioManager.Inst.LengthOfAudio(currentSong.audioTrack);
            AddSongDataToJson();
        }
    }

    public void DisplayNewData()
    {
        for (int i = 0; i < _clonedPrefabs.Count; i++)
        {
            _clonedPrefabs[i].GetComponent<SongPanel>().HighScore.text = So_SongData[i].HighScore.ToString();            
        }
    }

    private void LoadDataToSO()
    {
        for (int i = 0; i < gameData.songData.Count; i++)
        {            
            So_SongData[i].HighScore = gameData.songData[i].HighScore;            
        }
    }

    public void AddSongDataToJson()
    {
        gameData.songData.Clear();
        for (int i = 0; i < So_SongData.Count; i++)
        {
            SongData songData = new();
            songData.SongName = So_SongData[i].SongName;
            songData.HighScore = So_SongData[i].HighScore;
            gameData.songData.Add(songData);
        }
        SaveJsonData();
    }

    public void AddPlayerScore(int highScore)
    {        
        AddSongData currentSong = So_SongData.Find(u => u.SongName == _currentSong);        
        if(currentSong != null)
        {
            currentSong.HighScore = highScore;            
        }
    }

    public void CurrentSong(string songName)
    {
        _currentSong = songName;
        Debug.Log(_currentSong);        
    }

    public void SaveJsonData()
    {
        if (File.Exists(Path))
        {
            File.Delete(Path);
        }
        File.WriteAllText(Path, JsonUtility.ToJson(gameData));        
    }

    public void LoadJsonData()
    {
        if (File.Exists(Path))
        {                        
            string json = File.ReadAllText(Path);
            gameData = JsonUtility.FromJson<GameData>(json);

            Debug.Log("Json: " + json);
        }
        else
            Debug.LogError("File Doesn't Exist");
    }

    public void DisplaySongPanel()
    {
        for (int i = 0; i < So_SongData.Count; i++)
        {
            GameObject SongPanelClone = Instantiate(_songPanel, _parentContent.transform);
            SongPanel displaySongPanel = SongPanelClone.GetComponent<SongPanel>();             
            displaySongPanel.SongName.text = So_SongData[i].SongName;
            displaySongPanel.SongImage.sprite = So_SongData[i].SongImage;
            displaySongPanel.HighScore.text = ""+ So_SongData[i].HighScore;
            _clonedPrefabs.Add(SongPanelClone);
        }
    }
}

[System.Serializable]
public class GameData
{
    public List<SongData> songData;
}

[System.Serializable]
public class SongData
{
    public string SongName;
    public int HighScore;
}