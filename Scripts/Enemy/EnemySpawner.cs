using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    [SerializeField] GameObject knightPrefab;
    [SerializeField] GameObject archerPrefab;
    [SerializeField] GameObject giantPrefab;
    [SerializeField] GameObject roguePrefab;
    [SerializeField] GameObject priestPrefab;

    [SerializeField] List<Wave> waves;
    [SerializeField] List<GameObject> pathWaypoints;
    [SerializeField] LaneBehavior walkingLane;

    List<GameObject> enemiesToSpawn;
    int waveIndex;
    float nextSpawnTime;
    float delayBeforePreparationTimeStart = 5f;
    bool prepared;
    bool spawning;

    public delegate void WaveStatusUpdateAction();
    public event WaveStatusUpdateAction PreparationTimeBegin;
    public event WaveStatusUpdateAction WaveStart;

    // Start is called before the first frame update
    void Start()
    {
        enemiesToSpawn = new List<GameObject>();
        waveIndex = -1;
        nextSpawnTime = 0f;
        prepared = false;
        spawning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!prepared && !spawning && CharacterList.enemiesAlive.Count == 0 && waveIndex < waves.Count - 1)
        {
            Invoke(nameof(PrepareWave), delayBeforePreparationTimeStart);
            prepared = true;
        }
        if (enemiesToSpawn.Count > 0 && Time.time >= nextSpawnTime)
        {
            SendWave();
        }
    }

    void PrepareWave()
    {
        waveIndex++;
        nextSpawnTime = Time.time + waves[waveIndex].preparationTimeBeforeWave;
        PrepareEnemiesToSpawn();
        //PreparationTimeBegin();
    }

    void SendWave()
    {
        if (!spawning)
        {
            //WaveStart();
            spawning = true;
        }

        SpawnEnemy();
        nextSpawnTime = Time.time + waves[waveIndex].enemySpawnCooldown;

        if (enemiesToSpawn.Count == 0)
        {
            spawning = false;
            prepared = false;
        }
    }

    void PrepareEnemiesToSpawn()
    {
        PutEnemyPrefabAmmountOnListOfEnemiesToSpawn(knightPrefab, waves[waveIndex].numberOfKnights);
        PutEnemyPrefabAmmountOnListOfEnemiesToSpawn(archerPrefab, waves[waveIndex].numberOfArchers);
        PutEnemyPrefabAmmountOnListOfEnemiesToSpawn(giantPrefab, waves[waveIndex].numberOfGiants);
        PutEnemyPrefabAmmountOnListOfEnemiesToSpawn(roguePrefab, waves[waveIndex].numberOfRogues);
        PutEnemyPrefabAmmountOnListOfEnemiesToSpawn(priestPrefab, waves[waveIndex].numberOfPriests);
        ShuffleList(enemiesToSpawn);
    }

    void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemiesToSpawn[0], transform.position, Quaternion.identity);
        enemy.GetComponent<EnemyMovement>().Waypoints = pathWaypoints;
        enemy.GetComponent<EnemyMovement>().LaneBehavior = walkingLane;
        enemiesToSpawn.RemoveAt(0);
    }

    void PutEnemyPrefabAmmountOnListOfEnemiesToSpawn(GameObject enemyPrefab, int ammount)
    {
        for (int i = 0; i < ammount; i++)
        {
            enemiesToSpawn.Add(enemyPrefab);
        }
    }

    void ShuffleList(List<GameObject> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            GameObject temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
