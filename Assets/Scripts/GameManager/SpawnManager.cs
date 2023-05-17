using UnityEngine;
using System;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Inst;

    public Action OnSpawnTile;

    [SerializeField] private Transform _tileSpawnPointLeft;
    [SerializeField] private Transform _tileSpawnPointMiddle;
    [SerializeField] private Transform _tileSpawnPointRight;

    public List<GameObject> SpawnedList;
    private List<Vector3> _initialSpawnPointPositions;

    private int _nextBeatIndex = 0;

    public int NextBeatIndex { get => _nextBeatIndex; set => _nextBeatIndex = value; }

    private void Awake()
    {
        Inst = this;
        SpawnedList = new List<GameObject>();
        _initialSpawnPointPositions = new List<Vector3>();
        InitializeSpawnPointPositions();
    }

    private void Start()
    {
        SpawnTile();
    }

    private void OnEnable()
    {
        OnSpawnTile += SpawnTile;
    }

    private void Update()
    {
       // if (!IsInvoking(nameof(SpawnTile)))
         //   Invoke(nameof(SpawnTile), 1f);
    }

    private void InitializeSpawnPointPositions()
    {
        _initialSpawnPointPositions.Add(_tileSpawnPointLeft.position);
        _initialSpawnPointPositions.Add(_tileSpawnPointMiddle.position);
        _initialSpawnPointPositions.Add(_tileSpawnPointRight.position);
    }

    private void SpawnTile()
    {
        for (int i = 0; i < 8; i++)
        {
            GameObject tile = ObjectPooling.Inst.ObjectToPool();

            if (tile != null)
            {
                tile.SetActive(true);
                tile.transform.position = RandomSpawnGenerator().position;
                SpawnedList.Add(tile);
            }
        }
    }

    private Transform RandomSpawnGenerator()
    {
        float randomSpeed = UnityEngine.Random.Range(0.5f, 2);
        Vector3 newPosition = Vector3.back * randomSpeed;
        _tileSpawnPointLeft.position += newPosition;
        _tileSpawnPointMiddle.position += newPosition;
        _tileSpawnPointRight.position += newPosition;

        Transform[] transformArray = { _tileSpawnPointLeft, _tileSpawnPointMiddle, _tileSpawnPointRight };
        int randomTransformPoint = UnityEngine.Random.Range(0, transformArray.Length);
        return transformArray[randomTransformPoint];
    }

    public void ResetSpawnPoints()
    {
        _tileSpawnPointLeft.position = _initialSpawnPointPositions[0];
        _tileSpawnPointMiddle.position = _initialSpawnPointPositions[1];
        _tileSpawnPointRight.position = _initialSpawnPointPositions[2];
    }

    public void ResetSpawnedTiles()
    {
        foreach (GameObject tile in SpawnedList)
        {
            tile.SetActive(false);
        }
        SpawnedList.Clear();
    }
}


