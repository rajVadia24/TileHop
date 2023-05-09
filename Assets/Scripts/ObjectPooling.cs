using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling inst;

    [SerializeField] private int numberOfObjects;
    [SerializeField] private GameObject prefab;
    [SerializeField] private List<GameObject> listOfObjects;

    private void Awake()
    {
        inst = this;
    }

    private void Start()
    {
        CreateObjects();
    }

    public void CreateObjects()
    {
        listOfObjects = new();
        GameObject tmp;

        for (int i = 0; i < numberOfObjects; i++)
        {
            tmp = Instantiate(prefab);
            tmp.SetActive(false);
            listOfObjects.Add(tmp);
        }
    }

    public GameObject ObjectToPool()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            if (!listOfObjects[i].activeInHierarchy)
            {
                return listOfObjects[i];
            }
        }
        return null;
    }
}