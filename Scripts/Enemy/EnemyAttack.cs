using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class EnemyAttack : MonoBehaviour
{
    protected bool isAttacking;
    public bool IsAttacking { get => isAttacking; }

    abstract public bool CanAttack(Transform target);
    abstract public void Attack(GameObject target);
}
