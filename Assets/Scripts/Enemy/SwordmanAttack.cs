using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordmanAttack : MonoBehaviour
{

    EnemyMovement enemyMovement;

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
    }

    // Update is called once per frame
    void Update()
    {
        //Decreases attack Cooldown
        if(attackCooldownRemaining > 0) attackCooldownRemaining -= Time.deltaTime;
    }

    public bool CanAttack(){
        //Can only attack if distance is short and cooldown is up
        if(Vector2.Distance(this.transform.position, enemyMovement.playerPosition.position) < 0.2f 
            && attackCooldownRemaining <= 0.0f 
            && isAttacking == false){
                return true;
            }
        return false;
    }

    public void AttackPlayer(){
        //Activates Cooldown and set enemy as isAttacking
        attackCooldownRemaining += attackCooldown;
        isAttacking = true;
    }
}
