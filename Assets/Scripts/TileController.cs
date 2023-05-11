using UnityEngine;

public class TileController : MonoBehaviour
{    
    private void Update()
    {
        //if (!IsInvoking(nameof(ReUseTile)))
        //    Invoke(nameof(ReUseTile), 5);
    }

    private GameObject NextTilePosition()
    {
        int lenght = TileSpawnManager.inst._SpawnedList.Count;

        for(int i = 0; i < lenght; i++)
        {
            if(TileSpawnManager.inst._SpawnedList[i].gameObject == gameObject)
            {
                Debug.Log("Current Tile: " + i +"-" + gameObject.transform.position);
                GameObject nextTile = TileSpawnManager.inst._SpawnedList[i + 1];
                Debug.Log("Next Tile: " + (i + 1) + "-" + nextTile.transform.position);
                return nextTile;
            }            
        }
        return null;
    }

    private void ReUseTile()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Hit");
            TestBall.inst.GetNextTilePosition(NextTilePosition().transform.position);
            TileSpawnManager.inst._SpawnedList.Remove(gameObject);
            Invoke(nameof(ReUseTile), 2);
            //Debug.Log("NextTile: "+NextTilePosition().transform.position);
        }
    }
}