using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIPreparationTimeText : MonoBehaviour
{
    LevelManager levelManager;
    Text preparationTimeText;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = transform.parent.GetComponent<LevelManager>();
        preparationTimeText = GetComponent<Text>();
    }

    public void UpdateText()
    {
        preparationTimeText.text = "Tempo de preparação: " + levelManager.CurrentPreparationTime().ToString("F1") + "s";
    }
}
