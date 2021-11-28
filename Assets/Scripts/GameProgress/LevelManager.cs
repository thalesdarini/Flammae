using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] List<EnemySpawner> waveSpawners;
    [SerializeField] List<Gates> gates;
    [SerializeField] LevelChanger levelChanger;

    UIWaveCountText waveStartUI;
    UIPreparationTimeText preparationStartUI;
    Animator waveStartAnimation;
    Animator preparationStartAnimation;
    Animator gameLostAnimation;
    Animator gameWonAnimation;

    bool[] wasWaveCountAlreadyShown;
    bool[] wasPreparationTimeAlreadyShown;

    // Start is called before the first frame update
    void Start()
    {
        waveStartUI = transform.Find("WaveStart").GetComponent<UIWaveCountText>();
        preparationStartUI = transform.Find("PreparationStart").GetComponent<UIPreparationTimeText>();
        waveStartAnimation = waveStartUI.GetComponent<Animator>();
        preparationStartAnimation = preparationStartUI.GetComponent<Animator>();
        gameLostAnimation = transform.Find("GameLost").GetComponent<Animator>();
        gameWonAnimation = transform.Find("GameWon").GetComponent<Animator>();

        int numWaves = NumOfWaves();
        wasWaveCountAlreadyShown = new bool[numWaves];
        wasPreparationTimeAlreadyShown = new bool[numWaves];
        for (int i=0; i<numWaves; i++)
        {
            wasWaveCountAlreadyShown[i] = false;
            wasPreparationTimeAlreadyShown[i] = false;
        }

        AssignEvents();
    }

    // Update is called once per frame
    void Update()
    {
        bool isCurrentWaveOver = CheckIfWaveIsOver();

        if (isCurrentWaveOver)
        {
            if (CurrentWave() == NumOfWaves())
            {
                PlayerWon();
            }
        }

        foreach (EnemySpawner spawner in waveSpawners)
        {
            spawner.CanPrepare = isCurrentWaveOver;
        }
    }

    void AssignEvents()
    {
        foreach (EnemySpawner spawner in waveSpawners)
        {
            spawner.WaveStart += ShowWaveStart;
            spawner.PreparationTimeBegin += ShowPreparationStart;
        }

        foreach (Gates gates in gates)
        {
            gates.GateInvaded += PlayerLost;
        }
    }

    void ShowWaveStart()
    {
        int currentWaveIndex = CurrentWave() - 1;
        if (!wasWaveCountAlreadyShown[currentWaveIndex])
        {
            wasWaveCountAlreadyShown[currentWaveIndex] = true;
            waveStartUI.UpdateText();
            waveStartAnimation.SetTrigger("Play");
        }
    }

    void ShowPreparationStart()
    {
        int currentWaveIndex = CurrentWave() - 1;
        if (!wasPreparationTimeAlreadyShown[currentWaveIndex])
        {
            wasPreparationTimeAlreadyShown[currentWaveIndex] = true;
            preparationStartUI.UpdateText();
            preparationStartAnimation.SetTrigger("Play");
        }
    }

    void PlayerLost()
    {
        gameLostAnimation.SetTrigger("Play");
        Invoke(nameof(ResetLevelAfterSomeTime), 0.5f);
    }

    void ResetLevelAfterSomeTime()
    {
        levelChanger.ChangeTo(SceneManager.GetActiveScene().buildIndex);
    }

    void PlayerWon()
    {
        gameWonAnimation.SetTrigger("Play");
        Invoke(nameof(NextLevelAfterSomeTime), 0.5f);
    }

    void NextLevelAfterSomeTime()
    {
        levelChanger.ChangeTo(SceneManager.GetActiveScene().buildIndex + 1);
    }

    bool CheckIfWaveIsOver()
    {
        bool isSomeonePreparedOrSpawning = false;
        foreach (EnemySpawner spawner in waveSpawners)
        {
            if (spawner.Prepared || spawner.Spawning)
            {
                isSomeonePreparedOrSpawning = true;
                break;
            }
        }

        return (CharacterList.enemiesAlive.Count == 0 && !isSomeonePreparedOrSpawning);
    }

    public int NumOfWaves()
    {
        return waveSpawners[0].Waves.Count;
    }

    public int CurrentWave()
    {
        int currentWaveIndex = -1;
        foreach (EnemySpawner spawner in waveSpawners)
        {
            currentWaveIndex = Mathf.Max(currentWaveIndex, spawner.CurrentWaveIndex);
        }

        return currentWaveIndex + 1;
    }

    public float CurrentPreparationTime()
    {
        int currentWaveIndex = CurrentWave() - 1;
        float currentPreparationTime = Mathf.Infinity;
        foreach (EnemySpawner spawner in waveSpawners)
        {
            if (currentWaveIndex <= spawner.Waves.Count - 1)
            {
                int numberOfEnemies = spawner.Waves[currentWaveIndex].numberOfKnights + spawner.Waves[currentWaveIndex].numberOfArchers + spawner.Waves[currentWaveIndex].numberOfGiants + spawner.Waves[currentWaveIndex].numberOfRogues + spawner.Waves[currentWaveIndex].numberOfPriests;
                if (numberOfEnemies > 0)
                {
                    currentPreparationTime = Mathf.Min(currentPreparationTime, spawner.Waves[currentWaveIndex].preparationTimeBeforeWave);
                }
            }
        }

        return currentPreparationTime;
    }
}
