using System;
using System.Collections;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnEnemies : MonoBehaviour
{
    public static SpawnEnemies Instance;
    public GameObject[] spawnPositions;
    public float spawnTime = 10f;
    private float minSpawnTime = 1.5f;
    public GameObject enemyPrefab;

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }
    private void Awake()
    {
        Instance =  this;
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPos = spawnPositions[Random.Range(0, spawnPositions.Length)].transform.position;
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
    
    IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnTime);
        }
    }
    public void ReduceSpawnTime(float amount)
    {
        spawnTime = Mathf.Max(minSpawnTime, spawnTime - amount);
    }

    
}
