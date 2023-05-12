using UnityEngine;

public class TileController : MonoBehaviour
{    
    [SerializeField] private GameObject _parent;

    private float _rightLimit = 2;
    private int _direction = 1;
    private float _leftLimit = 2;
    private Vector3 _movement;
    private float _speed = 2;    
  
    private GameObject NextTilePosition()
    {
        int lenght = TileSpawnManager.Inst._SpawnedList.Count;

        for(int i = 0; i < lenght; i++)
        {
            if(TileSpawnManager.Inst._SpawnedList[i].gameObject == _parent)
            {
                Debug.Log("Current Tile: " + i +"-" + _parent.transform.position);
                GameObject nextTile = TileSpawnManager.Inst._SpawnedList[i + 1];
                Debug.Log("Next Tile: " + (i + 1) + "-" + nextTile.transform.position);
                return nextTile;
            }            
        }
        return null;
    }

    private void ReUseTile()
    {
        _parent.SetActive(false);        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {            
            Debug.Log("Player Hit");
            TestBall.Inst.GetNextTilePosition(NextTilePosition().transform.position);
            TileSpawnManager.Inst._SpawnedList.Remove(_parent);
            Invoke(nameof(ReUseTile), 1);            
        }
    }    
}