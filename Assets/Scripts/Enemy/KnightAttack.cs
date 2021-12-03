using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAttack : EnemyAttack
{

    [SerializeField] ContactFilter2D alliesFilter;
    [SerializeField] ContactFilter2D playerFilter;
    [SerializeField] GameObject sword;
    [SerializeField] float damage;
    [SerializeField] float attackCooldown;
    [SerializeField] float attackRange;
    [SerializeField] int maxHitsPerAttack;
    [SerializeField] float damageDelay;
    [SerializeField] float timeBetweenAttacks;

    Rigidbody2D rb2d;
    Animator attackAnimation;
    PolygonCollider2D swordCollider;
    AudioSource soundEffect;
    [SerializeField] GameObject healthBar;
    
    EnemyMovement enemyMovement;
    EnemyHealth enemyHealth;
    float attackCooldownRemaining;
    float damageDelayRemaining;
    int attacksRemaining;


    // Start is called before the first frame update
    void Start()
    {
        isAttacking = false;
        swordCollider = sword.GetComponent<PolygonCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        attackAnimation = GetComponent<Animator>();
        enemyMovement = GetComponent<EnemyMovement>();
        enemyHealth = GetComponent<EnemyHealth>();
        soundEffect = GetComponent<AudioSource>();
        attackCooldownRemaining = 0.0f;
        soundEffect.PlayOneShot(SoundManager.battlecry, 10);
    }

    // Update is called once per frame
    void Update()
    {
        //Decreases attack Cooldown
        if(attackCooldownRemaining > 0.0f) {
            attackCooldownRemaining -= Time.deltaTime;
            if(attackCooldownRemaining <= 0.0f) isAttacking = false;
        }

        if(isAttacking == true && damageDelayRemaining > 0.0f && !enemyHealth.IsKilled){
            damageDelayRemaining -= Time.deltaTime;
            if(damageDelayRemaining <= 0){
                doDamage();
                soundEffect.PlayOneShot(SoundManager.swordAttack, 5);
                attacksRemaining -= 1;
                if(attacksRemaining > 0) damageDelayRemaining = timeBetweenAttacks;
            }
        }
    }

    void doDamage(){

        Collider2D[] results = new Collider2D[maxHitsPerAttack];
        swordCollider.OverlapCollider(alliesFilter, results);
        swordCollider.OverlapCollider(playerFilter, results);
        foreach (Collider2D cd in results)
        {
            if (cd != null)
            {
                cd.gameObject.GetComponent<HealthScript>().TakeDamage(damage);
            }
        }
    }

    override public bool CanAttack(Transform target)
    {
        //Can only attack if distance is short and cooldown is up

        if(Vector2.Distance(this.transform.position, target.position) < attackRange 
            && enemyMovement.LaneBehavior.FoesInLane.Contains(target.gameObject)
            && attackCooldownRemaining <= 0.0f 
            && isAttacking == false) return true;

        return false;
    }

    override public void Attack(GameObject target)
    {
        //Activates Cooldown and set enemy as isAttacking
        rb2d.velocity = Vector2.zero;
        attackCooldownRemaining += attackCooldown;
        isAttacking = true;
        damageDelayRemaining += damageDelay;
        attacksRemaining = 3;

        if (target.transform.position.x - transform.position.x >= 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            healthBar.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            healthBar.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        attackAnimation.SetBool("isMoving", false);
        attackAnimation.SetTrigger("attack");
    }
}
