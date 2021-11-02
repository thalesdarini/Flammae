using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerUpgrader : TowerInteractor
{
    [SerializeField] Tower upgrade;

    public void UpgradeTower()
    {
        if (playerInArea.GetComponent<PlayerSoulCounter>().SpendSouls(upgrade.ConstructionCost))
        {
            windowUI.SetActive(false);
            Instantiate(upgrade.TowerPrefab, transform.position, transform.rotation);
            Destroy(transform.parent.gameObject);
        }
        else
        {
            callEventTowerPurchaseFail(upgrade.TowerName);
        }
    }

    public void DeconstructTower()
    {
        windowUI.SetActive(false);
        playerInArea.GetComponent<PlayerSoulCounter>().CollectSouls(transform.parent.GetComponent<Tower>().DeconstructionBonus);
        Instantiate(transform.parent.GetComponent<Tower>().TowerBuilderReference, transform.position, transform.rotation);
        Destroy(transform.parent.gameObject);
    }
}
