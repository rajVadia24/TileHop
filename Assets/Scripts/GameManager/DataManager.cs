using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Inst;

    [SerializeField] private GameObject _parentContent;
    [SerializeField] private GameObject _songPanel;

    public AddSongData So_SongData;
    //private AddSongData LoadedData;
    private List<GameObject> _clonedPrefabs = new();

    //public GameData gameData;

    private static string Path;

    private string _currentSong;

    private void Awake()
    {
        Inst = this;
        Path = Application.persistentDataPath + "/TileHop.json";
    }

    private void Start()
    {
        LoadJsonData();
        //LoadDataToSO();
        //AddDataFromSO();
        DisplaySongPanel();
    }

    private void OnApplicationQuit()
    {
        //SaveJsonData();
    }

    //public void AddDataFromSO()
    //{        
    //    AddSongData currentSong = So_SongData.Find(u => u.SongName == _currentSong);
    //    if (currentSong != null)
    //    {
    //        ScoreManager.Inst.HighScore = currentSong.HighScore;
    //        BallController.Inst.SoundToPlay(currentSong.audioTrack);
    //        AudioManager.Inst.LengthOfAudio(currentSong.audioTrack);
    //        AddSongDataToJson();
    //    }
    //}

    public void DisplayNewData()
    {
        for (int i = 0; i < _clonedPrefabs.Count; i++)
        {
            _clonedPrefabs[i].GetComponent<SongPanel>().HighScore.text = So_SongData.songData[i].HighScore.ToString();
        }
    }

    //private void LoadDataToSO()
    //{
    //    for (int i = 0; i < gameData.songData.Count; i++)
    //    {            
    //        So_SongData[i].HighScore = gameData.songData[i].HighScore;            
    //    }
    //}

    //public void AddSongDataFromSO()
    //{
    //    //gameData.songData.Clear();
    //    for (int i = 0; i < So_SongData.songData.Count; i++)
    //    {
    //        SongData songData = new();
    //        songData.SongName = So_SongData.songData[i].SongName;
    //        songData.HighScore = So_SongData.songData[i].HighScore;
    //        So_SongData.songData.Add(songData);
    //    }
    //    //SaveJsonData();
    //}


    public void AddPlayerScore(int highScore)
    {
        SongData currentSong = So_SongData.songData.Find(u => u.SongName == _currentSong);
        if (currentSong != null)
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
        File.WriteAllText(Path, JsonUtility.ToJson(So_SongData));
    }

    public void LoadJsonData()
    {
        if (File.Exists(Path))
        {
            string json = File.ReadAllText(Path);

            JsonUtility.FromJsonOverwrite(json, So_SongData); //<AddSongData>(json);//.songData[i].HighScore;                
            //for (int i = 0; i < So_SongData.songData.Count; i++)
            //{
            //}
            Debug.Log("Json: " + json);
        }
        else
            Debug.LogError("File Doesn't Exist");
    }

    public void DisplaySongPanel()
    {
        for (int i = 0; i < So_SongData.songData.Count; i++)
        {
            GameObject SongPanelClone = Instantiate(_songPanel, _parentContent.transform);
            SongPanel displaySongPanel = SongPanelClone.GetComponent<SongPanel>();
            displaySongPanel.SongName.text = So_SongData.songData[i].SongName;
            displaySongPanel.SongImage.sprite = So_SongData.songData[i].SongImage;
            displaySongPanel.HighScore.text = "" + So_SongData.songData[i].HighScore;
            _clonedPrefabs.Add(SongPanelClone);
        }
    }
}

//[System.Serializable]
//public class GameData
//{
//    public List<SongData> songData;
//}

//[System.Serializable]
//public class SongData
//{
//    public string SongName;
//    public int HighScore;
//}