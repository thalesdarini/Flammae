using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUIButton : MonoBehaviour
{
    [SerializeField] TowerBuilder towerBuilder;
    [SerializeField] TowerUpgrader towerUpgrader;

    public void BuildTowerCerberusPressed()
    {
        towerBuilder.BuildTowerCerberus();
    }

    public void UpgradeTowerPressed()
    {
        towerUpgrader.UpgradeTower();
    }

    public void DeconstructTowerPressed()
    {
        towerUpgrader.DeconstructTower();
    }
}
