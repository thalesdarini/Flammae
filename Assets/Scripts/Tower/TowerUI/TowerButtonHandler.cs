using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerButtonHandler : MonoBehaviour
{
    TowerBuilder towerBuilder;
    TowerUpgrader towerUpgrader;

    // Start is called before the first frame update
    void Start()
    {
        towerBuilder = transform.parent.parent.Find("AreaToInteract").gameObject.GetComponent<TowerBuilder>();
        towerUpgrader = transform.parent.parent.Find("AreaToInteract").gameObject.GetComponent<TowerUpgrader>();
    }

    public void BuildSpecificTowerPressed()
    {
        towerBuilder.BuildSpecificTower();
    }

    public void NextTowerPressed()
    {
        towerBuilder.NextTower();
    }

    public void PreviousTowerPressed()
    {
        towerBuilder.PreviousTower();
    }

    public void UpgradeTowerPressed()
    {
        towerUpgrader.UpgradeTower();
    }

    public void DeconstructTowerPressed()
    {
        towerUpgrader.DeconstructTower();
    }

    public void UpgradeTowerHoverEnter()
    {
        towerUpgrader.TowerUpgradeButtonHoverEnter();
    }

    public void UpgradeTowerHoverExit()
    {
        towerUpgrader.TowerUpgradeButtonHoverExit();
    }
}
