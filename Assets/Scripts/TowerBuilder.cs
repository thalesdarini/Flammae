using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerBuilder : TowerInteractor
{
    [SerializeField] Tower towerCerberus;

    public void BuildTowerCerberus()
    {
        if (playerInArea.GetComponent<PlayerSoulCounter>().SpendSouls(towerCerberus.ConstructionCost))
        {
            windowUI.SetActive(false);
            Instantiate(towerCerberus.TowerPrefab, transform.position, transform.rotation);
            Destroy(transform.parent.gameObject);
        }
        else
        {
            callEventTowerPurchaseFail(towerCerberus.TowerName);
        }
    }
}
