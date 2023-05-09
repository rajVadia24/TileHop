using UnityEngine;

public class TileSpawnManager : MonoBehaviour
{
    [SerializeField] private Transform _tileSpawnPointLeft;
    [SerializeField] private Transform _tileSpawnPointMiddle;
    [SerializeField] private Transform _tileSpawnPointRight;

    private void Update()
    {
        if (!IsInvoking(nameof(SpawnTile)))
            Invoke(nameof(SpawnTile), 1);
    }

    private void SpawnTile()
    {        
        GameObject tile = ObjectPooling.inst.ObjectToPool();        

        if (tile != null)
        {
            tile.transform.position = RandomSpawnGenerator().position;            
            tile.SetActive(true);
         
            //PlayerController.inst.SetControlPoint(tile.transform.position);
            //Test.inst.GetEndPoint(tile.transform.position);
        }
    }

    private Transform RandomSpawnGenerator()
    {
        Transform[] transformArray = { _tileSpawnPointLeft, _tileSpawnPointMiddle, _tileSpawnPointRight };
        int random = Random.Range(0, transformArray.Length);
        return transformArray[random];
    }
}
