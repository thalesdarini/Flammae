using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerButtonHandler : MonoBehaviour
{
    TowerBuilder towerBuilder;
    TowerUpgrader towerUpgrader;

    // Start is called before the first frame update
    private void Start()
    {
        towerBuilder = transform.parent.parent.Find("AreaToInteract").gameObject.GetComponent<TowerBuilder>();
        towerUpgrader = transform.parent.parent.Find("AreaToInteract").gameObject.GetComponent<TowerUpgrader>();
    }

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

    public void UpgradeTowerHoverEnter()
    {
        towerUpgrader.CallEventTowerUpgradeButtonHoverEnter();
    }

    public void UpgradeTowerHoverExit()
    {
        towerUpgrader.CallEventTowerUpgradeButtonHoverExit();
    }
}
