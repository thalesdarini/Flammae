using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class AttackScript : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] float defaultMeleeDamage;
    [SerializeField] float defaultRangedDamage;

    protected float meleeDamage;
    protected float rangedDamage;
    bool isBuffed;

    public bool IsBuffed { get => isBuffed; }

    // Start is called before the first frame update
    virtual protected void Start()
    {
        meleeDamage = defaultMeleeDamage;
        rangedDamage = defaultRangedDamage;
        isBuffed = false;
    }

    public void AttackModify(float buff)
    {
        meleeDamage += buff;
        rangedDamage += buff;

        if(meleeDamage == defaultMeleeDamage && rangedDamage == defaultRangedDamage)
        {
            isBuffed = false;
        }
        else
        {
            isBuffed = true;
        }
    }
}
