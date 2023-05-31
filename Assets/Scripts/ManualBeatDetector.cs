using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManualBeatDetector : MonoBehaviour
{
    public static ManualBeatDetector inst;
    
    public SpawnData SO_SpawnData;

    [SerializeField] private GameObject _spawnPrefab;
        
    private float _timer;
    private float _spawnTimer;

    private void Awake()
    {
        inst = this;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {                                   
            GameObject tile = Instantiate(_spawnPrefab);
            tile.transform.position = SpawnManager.Inst.RandomSpawnGenerator().position * (_timer * 10f);

            SO_SpawnData.Spawn.Add(tile.transform.position);
            _timer = 0;
        }        
    }

    private void SpawnOnBeats()
    {
        _timer += Time.deltaTime;

        Debug.Log("Pressed");
        float time = Mathf.Round(_timer * 100f) / 100f;
        float tileDistance = _timer / 10f;

        //for (int i = 0; i < SO_ClickData.Clicks.Count; i++)
        //{
        //    if (time == SO_ClickData.Clicks[i])
        //    {
        Debug.Log("SPAWN TILE");
        GameObject tile = Instantiate(_spawnPrefab);
        tile.transform.position = SpawnManager.Inst.RandomSpawnGenerator().position * tileDistance;
    }
}
    


