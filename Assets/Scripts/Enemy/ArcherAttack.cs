using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAttack : EnemyAttack
{
    [SerializeField] float attackCooldown;
    [SerializeField] float attackRange;
    [SerializeField] float damageDelay;
    [SerializeField] float damage;
    [SerializeField] float projectileSpeed;
    [SerializeField] float projectileTravelDistance;
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] GameObject arrowPlace;
    [SerializeField] GameObject healthBar;

    Rigidbody2D rb2d;
    Animator attackAnimation;
    AudioSource soundEffect;
    
    EnemyMovement enemyMovement;
    EnemyHealth enemyHealth;
    float attackCooldownRemaining;
    float damageDelayRemaining;
    Transform currentTarget;
    Vector2 aimDirection;

    // Start is called before the first frame update
    void Start()
    {
        isAttacking = false;
        rb2d = GetComponent<Rigidbody2D>();
        attackAnimation = GetComponent<Animator>();
        enemyMovement = GetComponent<EnemyMovement>();
        enemyHealth = GetComponent<EnemyHealth>();
        soundEffect = GetComponent<AudioSource>();
        currentTarget = null;
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

        if(isAttacking == true && damageDelayRemaining > 0.0f && !enemyHealth.IsKilled){
            damageDelayRemaining -= Time.deltaTime;
            if(damageDelayRemaining <= 0){
                aimDirection = currentTarget.position - arrowPlace.transform.position;
                soundEffect.PlayOneShot(SoundManager.bowShoot, 5);
                ShootArrow();
            }
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
        damageDelayRemaining = damageDelay;

        currentTarget = target.transform;

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

    public void ShootArrow()
    {
        GameObject proj = Instantiate(arrowPrefab, arrowPlace.transform.position, Quaternion.identity);
        proj.GetComponent<ArcherArrow>().SetProperties(damage, aimDirection, projectileSpeed, projectileTravelDistance);
    }
}
