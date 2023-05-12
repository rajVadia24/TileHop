using UnityEngine;
using System.Collections.Generic;   

public class TileSpawnManager : MonoBehaviour
{
    public static TileSpawnManager Inst;

    [SerializeField] private Transform _tileSpawnPointLeft;
    [SerializeField] private Transform _tileSpawnPointMiddle;
    [SerializeField] private Transform _tileSpawnPointRight;

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
        float randomSpeed = Random.Range(1, 3);
        _tileSpawnPointLeft.position += Vector3.back * randomSpeed;
        _tileSpawnPointMiddle.position += Vector3.back * randomSpeed;
        _tileSpawnPointRight.position += Vector3.back * randomSpeed;

        Transform[] transformArray = { _tileSpawnPointLeft, _tileSpawnPointMiddle, _tileSpawnPointRight };
        int randomTransformPoint = Random.Range(0, transformArray.Length);
        return transformArray[randomTransformPoint];
    }
}
