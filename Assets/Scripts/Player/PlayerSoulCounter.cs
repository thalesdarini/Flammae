using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoulCounter : MonoBehaviour
{
    [SerializeField] int startingSouls = 20;
    int numSouls;

    public int NumSouls { get => numSouls; }

    // Start is called before the first frame update
    void Start()
    {
        numSouls = startingSouls;
    }

    public void CollectSouls(int number)
    {
        numSouls += number;
    }

    public void LoseSouls(int number)
    {
        if (numSouls >= number)
        {
            numSouls -= number;
        }
        numSouls = 0;
    }

    public bool SpendSouls(int number)
    {
        if (numSouls >= number)
        {
            numSouls -= number;
            return true;
        }
        return false;
    }
}
