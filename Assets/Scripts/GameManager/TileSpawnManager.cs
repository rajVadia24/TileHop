using UnityEngine;
using System.Collections.Generic;

public class TileSpawnManager : MonoBehaviour
{
    public static TileSpawnManager inst;

    [SerializeField] private Transform _tileSpawnPointLeft;
    [SerializeField] private Transform _tileSpawnPointMiddle;
    [SerializeField] private Transform _tileSpawnPointRight;

    public List<GameObject> _SpawnedList;

    private void Awake()
    {
        inst = this;
    }

    private void Start()
    {
        //SpawnTile();
        SpawnTile();        
    }

    private void Update()
    {
        if (!IsInvoking(nameof(SpawnTile)))
            Invoke(nameof(SpawnTile), 0.5f);
    }

    private void SpawnTile()
    {        
        GameObject tile = ObjectPooling.inst.ObjectToPool();

        if (tile != null)
        {
            tile.SetActive(true);
            tile.transform.position = RandomSpawnGenerator().position;
            _SpawnedList.Add(tile);

            //BallController.inst.SetControlPoint(tile.transform.position);
            // BallControllerTest.inst.GetEndPosition(tile.transform.position);
        }        
    }
    
    private Transform RandomSpawnGenerator()
    {
        float randomSpeed = Random.Range(1, 3);
        _tileSpawnPointLeft.position += Vector3.back * randomSpeed;
        _tileSpawnPointMiddle.position += Vector3.back * randomSpeed;
        _tileSpawnPointRight.position += Vector3.back * randomSpeed;

        Transform[] transformArray = { _tileSpawnPointLeft, _tileSpawnPointMiddle, _tileSpawnPointRight };
        int randomTransformPoint = Random.Range(0, transformArray.Length);
        return transformArray[randomTransformPoint];
    }
}
