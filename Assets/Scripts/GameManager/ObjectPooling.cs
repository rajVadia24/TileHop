using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling Inst;

    [SerializeField] private int _numberOfEachObject;    
    [SerializeField] private GameObject _prefab;    

    public List<GameObject> ListOfObjects;

    private void Awake()
    {
        Inst = this;
    }

    private void Start()
    {
        CreateObjects();
    }

    public void CreateObjects()
    {
        ListOfObjects = new();
        GameObject tmp;

        for (int i = 0; i < _numberOfEachObject; i++)
        {
             tmp = Instantiate(_prefab);
             tmp.SetActive(false);
             ListOfObjects.Add(tmp);                                    
        }
    }   

    public GameObject ObjectToPool()
    {
        for (int i = 0; i < ListOfObjects.Count; i++)
        {
            if (!ListOfObjects[i].activeInHierarchy)
            {
                return ListOfObjects[i];
            }
        }
        return null;
    }
}