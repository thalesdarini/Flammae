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
        SoundManager.instance.PlaySoundEffect(SoundManager.button, 1.2f);
        towerBuilder.BuildSpecificTower();
    }

    public void NextTowerPressed()
    {
        SoundManager.instance.PlaySoundEffect(SoundManager.button, 1);
        towerBuilder.NextTower();
    }

    public void PreviousTowerPressed()
    {
        SoundManager.instance.PlaySoundEffect(SoundManager.button, 1);
        towerBuilder.PreviousTower();
    }

    public void UpgradeTowerPressed()
    {
        SoundManager.instance.PlaySoundEffect(SoundManager.button, 1.2f);
        towerUpgrader.UpgradeTower();
    }

    public void DeconstructTowerPressed()
    {
        SoundManager.instance.PlaySoundEffect(SoundManager.button, 1.2f);
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
