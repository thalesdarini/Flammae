using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerSoulBonusText : MonoBehaviour
{
    [SerializeField] Tower towerReference;
    Text text;

    void Awake()
    {
        text = GetComponent<Text>();

        text.text = "Recompensa: +" + towerReference.DeconstructionBonus.ToString() + " almas";
        text.color = new Color(214f / 256f, 212f / 256f, 0f / 256f);
    }
}
