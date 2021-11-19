using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAttack : EnemyAttack
{
    [SerializeField] float attackCooldown;

    Rigidbody2D rb2d;
    Animator attackAnimation;
    EnemyMovement enemyMovement;
    float attackCooldownRemaining;

    // Start is called before the first frame update
    void Start()
    {
        isAttacking = false;
        rb2d = GetComponent<Rigidbody2D>();
        attackAnimation = GetComponent<Animator>();
        enemyMovement = GetComponent<EnemyMovement>();
        attackCooldownRemaining = 0.0f;
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

    override public bool CanAttack(Transform target)
    {
        //Can only attack if distance is short and cooldown is up
        if (Vector2.Distance(this.transform.position, target.position) < 1.2f
            && enemyMovement.LaneBehavior.FoesInLane.Contains(target.gameObject)
            && attackCooldownRemaining <= 0.0f 
            && isAttacking == false){
                return true;
            }

        return false;
    }

    override public void Attack(GameObject target)
    {
        //Activates Cooldown and set enemy as isAttacking
        rb2d.velocity = Vector2.zero;
        attackCooldownRemaining += attackCooldown;
        isAttacking = true;

        attackAnimation.SetBool("isMoving", false);
        attackAnimation.SetTrigger("attack");
    }
}