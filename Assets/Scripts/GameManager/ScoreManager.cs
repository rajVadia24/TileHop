using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Inst;

    public StoreScoreData scoreData;

    [SerializeField] private TMP_Text _scoreText;

    private int _score = 0;

    public int Score { get { return scoreData.Score; } set { scoreData.Score = value; } }

    private void Awake()
    {
        Inst = this;
    }

    private void Start()
    {
        Score = 0;
        _scoreText.text = "Score: " + Score;
    }

    private void Update()
    {
        _scoreText.text = "Score: " + Score;
        Debug.Log("ScoreData ==> " + scoreData.Score);

        if(Score == 10){ BallController.Inst.ConstantSpeed = 1.8f; Debug.Log("Score is 10%"); }
        else if(Score == 30) { BallController.Inst.ConstantSpeed = 2.1f; Debug.Log("Score is 30%"); }
        else if(Score == 50) { BallController.Inst.ConstantSpeed = 2.5f; Debug.Log("Score is 50%"); }
        else if(Score == 70) { BallController.Inst.ConstantSpeed = 2.9f; Debug.Log("Score is 70%"); }
        else if(Score == 90) { BallController.Inst.ConstantSpeed = 3.5f; Debug.Log("Score is 90%"); }
    }
}
