using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] platformPrefabs;
    [SerializeField] private float[] platformSpawnRates;
    [SerializeField] private float levelWidth;
    [SerializeField] private float minY = 1f;
    [SerializeField] private float maxY = 3f;
    [SerializeField] private float spawnDistance = 5f;

    private Transform playerTransform;
    private float lastSpawnY;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        lastSpawnY = playerTransform.position.y - spawnDistance;
    }

    private void Update()
    {
        if (playerTransform.position.y > lastSpawnY - spawnDistance)
        {
            SpawnPlatforms();
        }
    }

    private void SpawnPlatforms()
    {
        for (int i = 0; i < platformPrefabs.Length; i++)
        {
            if (Random.value < platformSpawnRates[i])
            {
                GameObject platformInstance = Instantiate(platformPrefabs[i]);
                platformInstance.transform.position = new Vector3(Random.Range(-levelWidth, levelWidth), lastSpawnY + Random.Range(minY, maxY), 0);
                lastSpawnY = platformInstance.transform.position.y;
            }
        }
    }
}
