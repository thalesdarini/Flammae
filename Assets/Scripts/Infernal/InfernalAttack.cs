using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfernalAttack : AttackScript
{
    [Header("Attack")]
    [SerializeField] float attackRate;

    // current state variables
    bool isAttacking = false;
    GameObject currentTarget = null;

    // cached variables
    Animator animator;
    InfernalHealth infernalHealth;

    public bool IsAttacking { get => isAttacking; }

    override protected void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        animator.SetFloat("m_attack", 1 / attackRate);
        infernalHealth = GetComponent<InfernalHealth>();
    }

    private void Update()
    {
        if (currentTarget == null || infernalHealth.Dead)
        {
            StopAttacking();
        }
    }
    
    public void StartAttacking(GameObject enemy)
    {
        isAttacking = true;
        currentTarget = enemy;
        animator.SetBool("attacking", true);
    }

    private void StopAttacking()
    {
        isAttacking = false;
        currentTarget = null;
        animator.SetBool("attacking", false);
    }

    // called from animation
    public void Attack()
    {
        currentTarget?.GetComponent<EnemyHealth>().TakeDamage(meleeDamage);
    }
}
