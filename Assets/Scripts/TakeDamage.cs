using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    public void DamageMe()
    {
        GetComponent<Animator>().SetTrigger("Damage");
    }
}
