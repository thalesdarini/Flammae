using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAttack : EnemyAttack
{
    [SerializeField] float attackCooldown;
    [SerializeField] float attackRange;

    Rigidbody2D rb2d;
    Animator attackAnimation;
    
    EnemyMovement enemyMovement;
    float attackCooldownRemaining;
    

    // Start is called before the first frame update
    void Start()
    {
        isAttacking = false;
        attackCooldownRemaining = 0.0f;
        attackAnimation = GetComponent<Animator>();
        enemyMovement = GetComponent<EnemyMovement>();
        rb2d = GetComponent<Rigidbody2D>();
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

    public override bool CanAttack(Transform target){

        if(Vector2.Distance(this.transform.position, target.position) < attackRange 
            && attackCooldownRemaining <= 0.0f 
            && isAttacking == false) return true;

        return false;
    }

    public override void Attack(GameObject target){
        //Activates Cooldown and set enemy as isAttacking
        rb2d.velocity = Vector2.zero;
        attackCooldownRemaining += attackCooldown;
        isAttacking = true;

        attackAnimation.SetBool("isMoving", false);
        attackAnimation.SetTrigger("attack");
    }
}
