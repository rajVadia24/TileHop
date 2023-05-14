using UnityEngine;
using System.Collections.Generic;   

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Inst;

    [SerializeField] private Transform _tileSpawnPointLeft;
    [SerializeField] private Transform _tileSpawnPointMiddle;
    [SerializeField] private Transform _tileSpawnPointRight;

    private int _nextBeatIndex = 0;

    public int NextBeatIndex { get => _nextBeatIndex; set => _nextBeatIndex = value; }


    public List<GameObject> _SpawnedList;

    private void Awake()
    {
        Inst = this;
    }

    private void Start()
    {        
        SpawnTile();        
    }

    private void Update()
    {
        if (!IsInvoking(nameof(SpawnTile)))
            Invoke(nameof(SpawnTile), 1f);
    }

    private void SpawnTile()
    {
        for(int i = 0; i < 8; i++)
        {            
            GameObject tile = ObjectPooling.Inst.ObjectToPool();

            if (tile != null)
            {
                tile.SetActive(true);
                tile.transform.position = RandomSpawnGenerator().position;
                _SpawnedList.Add(tile);           
            }        
        }
    }

    private Transform RandomSpawnGenerator()
    {
        float randomSpeed = Random.Range(0.5f, 3);
        _tileSpawnPointLeft.position += Vector3.back * randomSpeed;
        _tileSpawnPointMiddle.position += Vector3.back * randomSpeed;
        _tileSpawnPointRight.position += Vector3.back * randomSpeed;

        Transform[] transformArray = { _tileSpawnPointLeft, _tileSpawnPointMiddle, _tileSpawnPointRight };
        int randomTransformPoint = Random.Range(0, transformArray.Length);
        return transformArray[randomTransformPoint];
    }

    //public Transform BeatBasedSpawnGenerator()
    //{
    //    Transform[] transformArray = { _tileSpawnPointLeft, _tileSpawnPointMiddle, _tileSpawnPointRight };

    //    // Use the next beat time
    //    float nextBeatTime = BeatDetector.Inst.BeatTimes[_nextBeatIndex];

    //    // Calculate the time until the next beat
    //    float timeUntilNextBeat = nextBeatTime - (Time.time - BallController.Inst.StartTime);

    //    // If the time until the next beat is negative, that means we missed the beat,
    //    // so we should use the next beat time instead
    //    if (timeUntilNextBeat < 0)
    //    {
    //        _nextBeatIndex++;
    //        if (_nextBeatIndex >= BeatDetector.Inst.BeatTimes.Length)
    //        {
    //            _nextBeatIndex = 0;
    //        }
    //        nextBeatTime = BeatDetector.Inst.BeatTimes[_nextBeatIndex];
    //        timeUntilNextBeat = nextBeatTime - (Time.time - BallController.Inst.StartTime);
    //    }

    //    // Calculate the speed based on the time until the next beat
    //    float speed = BallController.Inst.Distance / timeUntilNextBeat;

    //    // Move the spawn points based on the speed
    //    _tileSpawnPointLeft.position += Vector3.back * speed;
    //    _tileSpawnPointMiddle.position += Vector3.back * speed;
    //    _tileSpawnPointRight.position += Vector3.back * speed;

    //    // Choose a random spawn point
    //    int randomTransformPoint = Random.Range(0, transformArray.Length);
    //    return transformArray[randomTransformPoint];
    //}

}
