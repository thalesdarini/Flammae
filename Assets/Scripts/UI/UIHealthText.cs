using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIHealthText : MonoBehaviour
{
    PlayerHealth playerReference;
    Text healthText;

    // Start is called before the first frame update
    void Start()
    {
        playerReference = CharacterList.playersAlive[0].GetComponent<PlayerHealth>();
        healthText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = (playerReference.MaxHealth * playerReference.HealthPercentual).ToString("F0");
    }
}
