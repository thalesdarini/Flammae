using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerBuilder : TowerInteractor
{
    [SerializeField] List<Tower> buildableTowers;

    int currentTowerIndex;

    void Awake()
    {
        currentTowerIndex = 0;
        towerCurrentlyOnTextDisplay = buildableTowers[currentTowerIndex];
    }

    void InstantiateSpecificTower() { InstantiateBuilding(buildableTowers[currentTowerIndex].TowerPrefab); }
    public void BuildSpecificTower()
    {
        if (playerInArea.GetComponent<PlayerSoulCounter>().SpendSouls(buildableTowers[currentTowerIndex].ConstructionCost))
        {
            CreateBuilding(buildableTowers[currentTowerIndex].TowerPrefab, nameof(InstantiateSpecificTower), Mathf.Max(transform.parent.Find("Tower").localScale.x, buildableTowers[currentTowerIndex].transform.Find("Tower").localScale.x), buildableTowers[currentTowerIndex].ConstructionTime);
        }
        else
        {
            CallEventTowerPurchaseFail(buildableTowers[currentTowerIndex].TowerName);
        }
    }

    public void NextTower()
    {
        currentTowerIndex = (currentTowerIndex + 1) % buildableTowers.Count;

        towerCurrentlyOnTextDisplay = buildableTowers[currentTowerIndex];
        CallEventUpdateText();
    }

    public void PreviousTower()
    {
        currentTowerIndex = (currentTowerIndex - 1 + buildableTowers.Count) % buildableTowers.Count;

        towerCurrentlyOnTextDisplay = buildableTowers[currentTowerIndex];
        CallEventUpdateText();
    }
}
