using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerNameText : TowerFlashingText
{
    void Awake()
    {
        towerInteractorReference = transform.parent.parent.Find("AreaToInteract").GetComponent<TowerInteractor>();
        towerReference = towerInteractorReference.TowerCurrentlyOnTextDisplay;
        text = GetComponent<Text>();
        isFlashing = false;
        isUpdateable = true;
        baseColor = new Color(214f / 256f, 212f / 256f, 0f / 256f);

        DisplayText();
    }

    override public void DisplayText()
    {
        text.text = towerReference.TowerName;
    }
}
