using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIRangedAttackDamageText : MonoBehaviour
{
    PlayerAttack playerReference;
    Text healthText;

    // Start is called before the first frame update
    void Start()
    {
        playerReference = CharacterList.playersAlive[0].GetComponent<PlayerAttack>();
        healthText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Dano mágico: " + playerReference.RangedDamage.ToString("F1");
    }
}
