using UnityEngine;

public class TileController : MonoBehaviour
{    
    [SerializeField] private GameObject _parent;
         
    private GameObject NextTilePosition()
    {
        int lenght = SpawnManager.Inst._SpawnedList.Count;

        for(int i = 0; i < lenght; i++)
        {
            if(SpawnManager.Inst._SpawnedList[i] == _parent)
            {
                GameObject currentTilePosition = SpawnManager.Inst._SpawnedList[i];                
                //Debug.Log("Current Tile: " + i +"-" + _parent.transform.position);
                GameObject nextTile = SpawnManager.Inst._SpawnedList[i + 1];                
                //Debug.Log("Next Tile: " + (i + 1) + "-" + nextTile.transform.position);
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
            ScoreManager.Inst.Score += 1;
            //Debug.Log("Player Hit");
            BallController.Inst.GetNextTilePosition(NextTilePosition().transform.position);
            SpawnManager.Inst._SpawnedList.Remove(_parent);
            Invoke(nameof(ReUseTile), 1);            
        }
    }    
}