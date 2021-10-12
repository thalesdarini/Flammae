using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoulCounter : MonoBehaviour
{
    int numSouls;

    public void CollectSouls(int number)
    {
        numSouls += number;
    }

    public bool SpendSouls(int number)
    {
        if(numSouls >= number)
        {
            numSouls -= number;
            return true;
        }
        return false;
    }
}
