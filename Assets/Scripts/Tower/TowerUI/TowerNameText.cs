using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerNameText : TowerFlashingText
{
    [SerializeField] Tower towerReference;

    void Awake()
    {
        towerRef = towerReference;
        towerInteractorReference = transform.parent.parent.Find("AreaToInteract").GetComponent<TowerInteractor>();
        text = GetComponent<Text>();
        isFlashing = false;
        baseColor = new Color(214f / 256f, 212f / 256f, 0f / 256f);

        DisplayText();
    }

    override public void DisplayText()
    {
        text.text = towerRef.TowerName;
    }
}
