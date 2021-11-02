using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float firstSpawnTime = 10f;
    public GameObject enemyPrefab;
    private float spawnTime;
    private float lastTimeSpawn;
    private float enemySpawnCooldown = 2.5f;
    private float spawnCount = 0;
    private float currSpawnSize = 4;
    private float gameTime;

    void Start()
    {
        gameTime = 0f;
        spawnTime = firstSpawnTime;
        lastTimeSpawn = firstSpawnTime-1;
    }

    void FixedUpdate()
    {
        gameTime += Time.deltaTime;

        if(gameTime > spawnTime && gameTime > lastTimeSpawn + enemySpawnCooldown)
        {
            GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity) as GameObject;
            lastTimeSpawn = gameTime;
            spawnCount++;
            if(spawnCount > currSpawnSize)
            {
                if(enemySpawnCooldown > 0.6f) enemySpawnCooldown -= 0.8f;
                spawnCount = 0;
                spawnTime += 35;
                currSpawnSize += 2;
            }
        }
    }
}
