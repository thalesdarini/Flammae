using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordmanAttack : MonoBehaviour
{
    public bool IsAttacking { get => isAttacking; }

    Animator attackAnimation;
    EnemyMovement enemyMovement;
    GameObject player;

    [SerializeField]
    float attackCooldown;
    
    float attackCooldownRemaining;
    bool isAttacking;

    // Start is called before the first frame update
    void Start()
    {
        isAttacking = false;
        attackCooldownRemaining = 0.0f;
        enemyMovement = FindObjectOfType<EnemyMovement>();
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
        //Can only attack if distance is short and cooldown is up

        if(Vector2.Distance(this.transform.position, target.position) < 1.2f 
            && attackCooldownRemaining <= 0.0f 
            && isAttacking == false){
                return true;
            }

        return false;
    }

    public void AttackPlayer(GameObject target){
        //Activates Cooldown and set enemy as isAttacking
        attackCooldownRemaining += attackCooldown;
        isAttacking = true;

        attackAnimation.SetBool("isMoving", false);
        attackAnimation.SetTrigger("attack");

        
    }
}
