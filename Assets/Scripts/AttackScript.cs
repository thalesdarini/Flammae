using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class AttackScript : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] float defaultDamage;

    protected float damage;
    bool isBuffed;

    public bool IsBuffed { get => isBuffed; }

    // Start is called before the first frame update
    virtual protected void Start()
    {
        damage = defaultDamage;
        isBuffed = false;
    }

    public void AttackModify(float buff)
    {
        damage += buff;

        if(damage == defaultDamage)
        {
            isBuffed = false;
        }
        else
        {
            isBuffed = true;
        }
    }
}
