using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerSoulBonusText : TowerFlashingText
{
    void Awake()
    {
        towerInteractorReference = transform.parent.parent.parent.Find("AreaToInteract").GetComponent<TowerInteractor>();
        towerReference = towerInteractorReference.TowerCurrentlyOnTextDisplay;
        text = GetComponent<Text>();
        isFlashing = true;
        isUpdateable = towerInteractorReference is TowerBuilder ? true : false;
        baseColor = new Color(214f / 256f, 212f / 256f, 0f / 256f);

        DisplayText();
    }

    override public void DisplayText()
    {
        text.text = "+" + towerReference.DeconstructionBonus.ToString();
    }
}
