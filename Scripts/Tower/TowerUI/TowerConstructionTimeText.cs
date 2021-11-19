using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerConstructionTimeText : TowerFlashingText
{
    void Awake()
    {
        towerInteractorReference = transform.parent.parent.Find("AreaToInteract").GetComponent<TowerInteractor>();
        towerReference = towerInteractorReference is TowerBuilder ? towerInteractorReference.TowerCurrentlyOnTextDisplay : ((TowerUpgrader)towerInteractorReference).Upgrade;
        text = GetComponent<Text>();
        isFlashing = false;
        isUpdateable = towerInteractorReference is TowerBuilder ? true : false;
        baseColor = new Color(255f / 256f, 18f / 256f, 18f / 256f);

        DisplayText();
    }

    override public void DisplayText()
    {
        text.text = "(" + towerReference.ConstructionTime.ToString() + "s)";
    }
}
