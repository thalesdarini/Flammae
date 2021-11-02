using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerNameText : TowerFlashingText
{
    [SerializeField] Tower towerRef;

    void Awake()
    {
        towerReference = towerRef;
        towerInteractorReference = transform.parent.parent.parent.Find("AreaToInteract").GetComponent<TowerInteractor>();
        text = GetComponent<Text>();
        isFlashing = false;

        text.text = towerReference.TowerName;
    }
}
