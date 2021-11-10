using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerBuilder : TowerInteractor
{
    [SerializeField] Tower towerCerberus;

    void InstantiateTowerCerberus() { InstantiateBuilding(towerCerberus.TowerPrefab); }
    public void BuildTowerCerberus()
    {
        if (playerInArea.GetComponent<PlayerSoulCounter>().SpendSouls(towerCerberus.ConstructionCost))
        {
            CreateBuilding(towerCerberus.TowerPrefab, nameof(InstantiateTowerCerberus), Mathf.Max(transform.parent.Find("Tower").localScale.x, towerCerberus.transform.Find("Tower").localScale.x), towerCerberus.ConstructionTime);
        }
        else
        {
            CallEventTowerPurchaseFail(towerCerberus.TowerName);
        }
    }
}
