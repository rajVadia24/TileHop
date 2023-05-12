using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling Inst;

    [SerializeField] private int _numberOfEachObject;    
    [SerializeField] private GameObject[] _prefabs;    

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
            for(int j = 0; j < _prefabs.Length; j++)
            {
                tmp = Instantiate(_prefabs[j]);
                tmp.SetActive(false);
                ListOfObjects.Add(tmp);
            }
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