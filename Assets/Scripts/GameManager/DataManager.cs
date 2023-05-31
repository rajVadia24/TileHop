using System.Collections.Generic;
using System.IO;
using UnityEngine;
using DG.Tweening;

public class DataManager : MonoBehaviour
{
    public static DataManager Inst;

    [SerializeField] private GameObject _parentContent;
    [SerializeField] private GameObject _songPanel;

    public AddSongData So_SongData;
    private List<GameObject> _clonedPrefabs = new();    

    private static string Path;
    private static string SongDataPath;

    private string _currentSong;
    private string _songToPlay;

    private void Awake()
    {
        Inst = this;
        Path = Application.persistentDataPath + "/TileHop.json";        
    }

    private void Start()
    {
        LoadJsonData();     
        DisplaySongPanel();
        CdController.inst.AudioToPlay(So_SongData.songData[0].audioTrack, So_SongData.songData[0].SongImage);
    }

    //private void OnApplicationQuit()
    //{
    //    //SaveJsonData();
    //}    

    public void DisplayNewData()
    {        
        for (int i = 0; i < _clonedPrefabs.Count; i++)
        {         
            _clonedPrefabs[i].GetComponent<SongPanel>().HighScore.text = So_SongData.songData[i].HighScore.ToString();
        }            
    }
   
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
        SongData currentSong = So_SongData.songData.Find(u => u.SongName == _currentSong);
        BallController.Inst.SoundToPlay(currentSong.audioTrack);
        AudioManager.Inst.LengthOfAudio(currentSong.audioTrack);
    }

    public void SongToBePlayed(string songName)
    {
        _songToPlay = songName;
        SongData currentSong = So_SongData.songData.Find(u => u.SongName == _songToPlay);
        CdController.inst.AudioToPlay(currentSong.audioTrack, currentSong.SongImage);
        Debug.Log("Image==> " + currentSong.SongImage);
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

            JsonUtility.FromJsonOverwrite(json, So_SongData);                
           
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
            SongPanelClone.transform.DOScale(Vector3.one, 1f).SetEase(Ease.OutFlash);
            SongPanel displaySongPanel = SongPanelClone.GetComponent<SongPanel>();
            displaySongPanel.SongName.text = So_SongData.songData[i].SongName;
            displaySongPanel.SongImage.sprite = So_SongData.songData[i].SongImage;
            displaySongPanel.HighScore.text = "" + So_SongData.songData[i].HighScore;
            _clonedPrefabs.Add(SongPanelClone);

            Debug.Log("SONG DATA COUNT: "+So_SongData.songData.Count);
        }
    }
}