using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Inst;    

    [SerializeField] private GameObject _parentContent;
    [SerializeField] private GameObject _songPanel;

    public List<AddSongData> So_SongData;    

    //public PlayerData playerData;

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
        //LoadJsonData();
        AddDataFromSO();
        DisplaySongPanel();
    }

    public void AddDataFromSO()
    {
        //for(int i=0; i<So_SongData.Count; i++)
        //{

        //    //AddSongData(So_SongData[i].SongName, So_SongData[i].SongImage);
        //}
        AddSongData currentSong = So_SongData.Find(u => u.SongName == _currentSong);
        if (currentSong != null)
        {
            ScoreManager.Inst.HighScore = currentSong.HighScore;
            BallController.Inst.SoundToPlay(currentSong.audioTrack);
            AudioManager.Inst.LengthOfAudio(currentSong.audioTrack);
        }
    }

    //public void AddSongData(string songName, Sprite songImage)
    //{
    //    SongData songData = new();
    //    songData.SongName = songName;
    //    songData.SongImage = songImage;
    //    songData.scoreData = new();

    //    playerData.songData.Add(songData);
    //}

    public void AddPlayerScore(int highScore)
    {        
        //SongData currentSong = playerData.songData.Find(u => u.SongName == _currentSong);
        //Debug.Log("Current: " + currentSong.SongName);
        //if(currentSong != null)
        //{
        //    currentSong.scoreData.Score = score;
        //    currentSong.scoreData.HighScore = highScore;
        //}

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

    //public void SaveJsonData()
    //{
    //    string path = Path;
    //    File.WriteAllText(path, JsonUtility.ToJson(playerData));
    //}

    //public void LoadJsonData()
    //{
    //    if (File.Exists(Path))
    //    {
    //        string json = File.ReadAllText(Path);
    //        playerData = JsonUtility.FromJson<PlayerData>(json);

    //        Debug.Log("Json: " + json);
    //    }
    //    else
    //        Debug.LogError("File Doesn't Exist");
    //}

    public void DisplaySongPanel()
    {
        for (int i = 0; i < So_SongData.Count; i++)
        {
            GameObject songPanelClone = Instantiate(_songPanel, _parentContent.transform);
            SongPanel displaySongPanel = songPanelClone.GetComponent<SongPanel>();
            displaySongPanel.SongName.text = So_SongData[i].SongName;
            displaySongPanel.SongImage.sprite = So_SongData[i].SongImage;
            displaySongPanel.HighScore.text = ""+ So_SongData[i].HighScore;
        }
    }
}

//[System.Serializable]
//public class PlayerData
//{
//    public List<SongData> songData = new();
//}

//[System.Serializable]
//public class SongData
//{
//    public string SongName;
//    public Sprite SongImage;
//    public ScoreData scoreData = new();
//}

//[System.Serializable]
//public class ScoreData
//{
//    public string Score;
//    public string HighScore;
//}
