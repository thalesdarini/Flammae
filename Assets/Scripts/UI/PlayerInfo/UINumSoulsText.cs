using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UINumSoulsText : MonoBehaviour
{
    PlayerSoulCounter playerReference;
    Text soulText;

    // Start is called before the first frame update
    void Start()
    {
        playerReference = CharacterList.playersAlive[0].GetComponent<PlayerSoulCounter>();
        soulText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        soulText.text = playerReference.NumSouls.ToString();
    }
}
