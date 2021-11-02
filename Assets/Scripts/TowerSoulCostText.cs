using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerSoulCostText : TowerFlashingText
{
    [SerializeField] Tower towerRef;

    void Awake()
    {
        towerReference = towerRef;
        towerInteractorReference = transform.parent.parent.Find("AreaToInteract").GetComponent<TowerInteractor>();
        text = GetComponent<Text>();
        isFlashing = false;

        text.text = "Sacrifício: " + towerReference.ConstructionCost.ToString() + " almas";
    }
}
