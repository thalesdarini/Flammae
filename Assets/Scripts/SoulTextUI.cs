using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SoulTextUI : MonoBehaviour
{
    [SerializeField] Text soulText;

    // Start is called before the first frame update
    void Awake()
    {
        soulText = GetComponent<Text>();
    }

    public void UpdateText(int qte)
    {
        soulText.text = "Almas: " + qte;
    }
}
