using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemies")]
    [SerializeField] GameObject enemyPrefab;

    [Header("Wave Times")]
    [SerializeField] float firstWaveStartingTime;
    [SerializeField] float timeBetweenWaves;

    [Header("Wave Sizes")]
    [SerializeField] int initialSpawnSize;
    [SerializeField] int spawnSizeIncrease;
    [SerializeField] int maximumSpawnSize;

    [Header("Enemy Spawn Cooldown")]
    [SerializeField] float initialEnemySpawnCooldown;
    [SerializeField] float enemySpawnCooldownReduction;
    [SerializeField] float minimumEnemySpawnCooldown;

    [SerializeField] List <GameObject> waypoints;

    float spawnTime;
    float enemySpawnCooldown;
    float lastTimeSpawn;
    float gameTime;
    int spawnCount;
    int currSpawnSize;

    void Start()
    {
        spawnTime = firstWaveStartingTime;
        enemySpawnCooldown = initialEnemySpawnCooldown;
        lastTimeSpawn = firstWaveStartingTime - enemySpawnCooldown;
        gameTime = 0f;
        spawnCount = 0;
        currSpawnSize = initialSpawnSize;
    }

    void FixedUpdate()
    {
        gameTime += Time.deltaTime;

        if(gameTime > spawnTime && gameTime > lastTimeSpawn + enemySpawnCooldown)
        {
            GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity) as GameObject;
            enemy.GetComponent<EnemyMovement>().Waypoints = waypoints;
            lastTimeSpawn = gameTime;
            spawnCount++;
            if(spawnCount >= currSpawnSize)
            {
                spawnCount = 0;
                spawnTime += timeBetweenWaves;

                if (enemySpawnCooldown - enemySpawnCooldownReduction >= minimumEnemySpawnCooldown)
                {
                    enemySpawnCooldown -= enemySpawnCooldownReduction;
                }
                else
                {
                    enemySpawnCooldown = minimumEnemySpawnCooldown;
                }

                if (currSpawnSize + spawnSizeIncrease <= maximumSpawnSize)
                {
                    currSpawnSize += spawnSizeIncrease;
                }
                else
                {
                    currSpawnSize = maximumSpawnSize;
                }
            }
        }
    }
}
