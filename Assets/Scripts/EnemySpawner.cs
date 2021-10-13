using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public float firstSpawnTime = 10f;
    private float spawnTime;
    private float lastTimeSpawn;
    private float enemySpawnCooldown = 2.5f;
    private float spawnCount = 0;
    private float currSpawnSize = 4;
    public GameObject enemyPrefab;
    public float tempoDeJogo;

    void start(){
        tempoDeJogo = 0f;
        spawnTime = firstSpawnTime;
        lastTimeSpawn = firstSpawnTime-1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        tempoDeJogo += Time.deltaTime;

        if(tempoDeJogo > spawnTime && tempoDeJogo > lastTimeSpawn + enemySpawnCooldown){
            GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity) as GameObject;
            lastTimeSpawn = tempoDeJogo;
            spawnCount++;
            if(spawnCount > currSpawnSize){
                if(enemySpawnCooldown > 0.6f) enemySpawnCooldown -= 0.8f;
                spawnCount = 0;
                spawnTime += 35;
                currSpawnSize += 2;
            }
        }
    }
}
