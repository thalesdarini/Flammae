using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Wave", order = 1)]
public class Wave : ScriptableObject
{
    public float preparationTimeBeforeWave;
    public float enemySpawnCooldown;

    [Header("Enemy Ammount of Each Type")]
    public int numberOfKnights;
    public int numberOfArchers;
    public int numberOfGiants;
    public int numberOfRogues;
    public int numberOfPriests;
}
