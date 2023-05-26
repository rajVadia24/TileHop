using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Inst;     

    public TMP_Text _scoreText;    

    private int _score = 0;
    private int _highScore;        
    
    public int Score { get { return _score; } set { _score = value; if (_score > _highScore) { _highScore = _score; } } }
    public int HighScore { get { return _highScore; } set { _highScore = value; } }

    private void Awake()
    {
        Inst = this;
    }

    private void Start()
    {        
        _scoreText.text = "Score: " + Score;        
    }               
}
