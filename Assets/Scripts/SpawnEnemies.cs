using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject[] spawnPositions;
    private float spawnTime = 5;
    private float spawnStartTime;
    public GameObject enemyPrefab;

    void SpawnEnemy()
    {
        Vector3 spawnPos = spawnPositions[Random.Range(0, spawnPositions.Length)].transform.position;
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        spawnTime = spawnStartTime;
    }

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy),0, spawnTime);
    }
}
