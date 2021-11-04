using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerSoulBonusText : TowerFlashingText
{
    [SerializeField] Tower towerReference;

    void Awake()
    {
        towerRef = towerReference;
        towerInteractorReference = transform.parent.parent.parent.Find("AreaToInteract").GetComponent<TowerInteractor>();
        text = GetComponent<Text>();
        isFlashing = true;
        baseColor = new Color(214f / 256f, 212f / 256f, 0f / 256f);

        DisplayText();
    }

    override public void DisplayText()
    {
        text.text = "+" + towerRef.DeconstructionBonus.ToString();
    }
}
