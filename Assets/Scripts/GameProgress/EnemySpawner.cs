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
    int currentWaveIndex;
    float nextSpawnTime;
    float delayBeforePreparationTimeStart = 2.5f;
    bool canPrepare;
    bool prepared;
    bool spawning;

    public List<Wave> Waves { get => waves; }
    public int CurrentWaveIndex { get => currentWaveIndex; }
    public bool CanPrepare { set => canPrepare = value; }
    public bool Prepared { get => prepared; }
    public bool Spawning { get => spawning; }

    public delegate void WaveStatusUpdateAction();
    public event WaveStatusUpdateAction PreparationTimeBegin;
    public event WaveStatusUpdateAction WaveStart;

    // Start is called before the first frame update
    void Start()
    {
        enemiesToSpawn = new List<GameObject>();
        currentWaveIndex = -1;
        nextSpawnTime = 0f;
        canPrepare = false;
        prepared = false;
        spawning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canPrepare && currentWaveIndex < waves.Count - 1)
        {
            PrepareWave();
        }
        if (prepared && enemiesToSpawn.Count >= 0 && Time.time >= nextSpawnTime)
        {
            SendWave();
        }
    }

    void PrepareWave()
    {
        currentWaveIndex++;
        prepared = true;
        nextSpawnTime = Time.time + waves[currentWaveIndex].preparationTimeBeforeWave + delayBeforePreparationTimeStart;
        Invoke(nameof(CallPreparationTimeBeginEvent), delayBeforePreparationTimeStart);
        PrepareEnemiesToSpawn();
    }

    void CallPreparationTimeBeginEvent()
    {
        PreparationTimeBegin();
    }

    void SendWave()
    {
        if (enemiesToSpawn.Count == 0)
        {
            spawning = false;
            prepared = false;
            return;
        }

        if (!spawning)
        {
            WaveStart();
            spawning = true;
        }

        SpawnEnemy();
        nextSpawnTime = Time.time + waves[currentWaveIndex].enemySpawnCooldown;
    }

    void PrepareEnemiesToSpawn()
    {
        PutEnemyPrefabAmmountOnListOfEnemiesToSpawn(knightPrefab, waves[currentWaveIndex].numberOfKnights);
        PutEnemyPrefabAmmountOnListOfEnemiesToSpawn(archerPrefab, waves[currentWaveIndex].numberOfArchers);
        PutEnemyPrefabAmmountOnListOfEnemiesToSpawn(giantPrefab, waves[currentWaveIndex].numberOfGiants);
        PutEnemyPrefabAmmountOnListOfEnemiesToSpawn(roguePrefab, waves[currentWaveIndex].numberOfRogues);
        PutEnemyPrefabAmmountOnListOfEnemiesToSpawn(priestPrefab, waves[currentWaveIndex].numberOfPriests);
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
