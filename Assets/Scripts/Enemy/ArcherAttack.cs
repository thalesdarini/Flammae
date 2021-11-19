using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAttack : MonoBehaviour
{
public bool IsAttacking { get => isAttacking; }

    Animator attackAnimation;

    [SerializeField]
    float attackCooldown;

    [SerializeField]
    float attackRange;
    
    float attackCooldownRemaining;
    bool isAttacking;

    // Start is called before the first frame update
    void Start()
    {
        isAttacking = false;
        attackCooldownRemaining = 0.0f;
        attackAnimation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Decreases attack Cooldown
        if(attackCooldownRemaining > 0.0f) {
            attackCooldownRemaining -= Time.deltaTime;
            if(attackCooldownRemaining <= 0.0f) isAttacking = false;
        }
    }

    public bool CanAttack(Transform target){

        if(Vector2.Distance(this.transform.position, target.position) < attackRange 
            && attackCooldownRemaining <= 0.0f 
            && isAttacking == false) return true;

        return false;
    }

    public void AttackFoe(GameObject target){
        //Activates Cooldown and set enemy as isAttacking
        attackCooldownRemaining += attackCooldown;
        isAttacking = true;

        attackAnimation.SetBool("isMoving", false);
        attackAnimation.SetTrigger("attack");

        
    }
}
