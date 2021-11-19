using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerUpgrader : TowerInteractor
{
    [SerializeField] Tower upgrade;

    public Tower Upgrade { get => upgrade; }

    void Awake()
    {
        towerCurrentlyOnTextDisplay = transform.parent.GetComponent<Tower>();
    }

    void InstantiateTowerUpgrade() { InstantiateBuilding(upgrade.TowerPrefab); }
    public void UpgradeTower()
    {
        if (playerInArea.GetComponent<PlayerSoulCounter>().SpendSouls(upgrade.ConstructionCost))
        {
            CreateBuilding(upgrade.TowerPrefab, nameof(InstantiateTowerUpgrade), Mathf.Max(transform.parent.Find("Tower").localScale.x, upgrade.transform.Find("Tower").localScale.x), upgrade.ConstructionTime);
        }
        else
        {
            CallEventTowerPurchaseFail(upgrade.TowerName);
        }
    }

    void InstantiateTowerBuilder() { InstantiateBuilding(transform.parent.GetComponent<Tower>().TowerBuilderReference); }
    public void DeconstructTower()
    {
        playerInArea.GetComponent<PlayerSoulCounter>().CollectSouls(transform.parent.GetComponent<Tower>().DeconstructionBonus);
        CreateBuilding(transform.parent.GetComponent<Tower>().TowerBuilderReference, nameof(InstantiateTowerBuilder), Mathf.Max(transform.parent.Find("Tower").localScale.x, transform.parent.GetComponent<Tower>().TowerBuilderReference.transform.Find("Tower").localScale.x), 0.7f);
    }

    public void TowerUpgradeButtonHoverEnter()
    {
        towerCurrentlyOnTextDisplay = upgrade;
        CallEventUpdateText();
    }

    public void TowerUpgradeButtonHoverExit()
    {
        towerCurrentlyOnTextDisplay = transform.parent.GetComponent<Tower>();
        CallEventUpdateText();
    }
}
