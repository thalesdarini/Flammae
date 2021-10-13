using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoulCounter : MonoBehaviour
{
    SoulTextUI soulTextUI;
    int numSouls;

    private void Awake()
    {
        numSouls = 2;
    }

    private void Start()
    {
        soulTextUI = FindObjectOfType<SoulTextUI>();
        soulTextUI.UpdateText(numSouls);
    }

    public void CollectSouls(int number)
    {
        numSouls += number;
        soulTextUI.UpdateText(numSouls);
    }

    public bool SpendSouls(int number)
    {
        if(numSouls >= number)
        {
            numSouls -= number;
            soulTextUI.UpdateText(numSouls);
            return true;
        }
        return false;
    }
}
