using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerTextUpgradeModifier : MonoBehaviour
{
    [SerializeField] Tower currentTower;
    [SerializeField] Tower upgradedTower;

    TowerUpgrader towerUpgraderReference;
    TowerFlashingText towerTextScriptReference;

    void Awake()
    {
        towerUpgraderReference = transform.parent.parent.Find("AreaToInteract").GetComponent<TowerUpgrader>();
        towerTextScriptReference = GetComponent<TowerFlashingText>();
    }

    void OnEnable()
    {
        towerUpgraderReference.TowerUpgradeButtonHoverEnter += ChangeNameTextToUpgrade;
        towerUpgraderReference.TowerUpgradeButtonHoverExit += ChangeNameTextToCurrent;
    }

    void OnDisable()
    {
        towerUpgraderReference.TowerUpgradeButtonHoverEnter -= ChangeNameTextToUpgrade;
        towerUpgraderReference.TowerUpgradeButtonHoverExit -= ChangeNameTextToCurrent;
    }

    void ChangeNameTextToUpgrade()
    {
        towerTextScriptReference.TowerRef = upgradedTower;
        towerTextScriptReference.DisplayText();
    }

    void ChangeNameTextToCurrent()
    {
        towerTextScriptReference.TowerRef = currentTower;
        towerTextScriptReference.DisplayText();
    }
}
