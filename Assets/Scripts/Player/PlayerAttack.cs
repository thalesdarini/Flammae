using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : AttackScript
{
    [SerializeField] ContactFilter2D enemyFilter;

    [Header("Sword attack")]
    [SerializeField] GameObject sword;
    [SerializeField] float meleeTimeInputTolerance;
    [SerializeField] float meleeAttackDuration;
    [SerializeField] int meleeMaxEnemiesHit;

    [Header("Ranged attack")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] GameObject projectilePlace;
    [SerializeField] float rangedTimeInputTolerance;
    [SerializeField] float rangedAttackDuration;
    [SerializeField] float projectileSpeed;
    [SerializeField] float projectileTravelDistance;

    // Input variables
    Vector2 aimDirection;
    float meleeAttackTimeInput = -1f; // just below 0 so it doesn't start attacking
    float rangedAttackTimeInput = -1f;

    // Current state variables
    bool canAttack = true;
    bool isAttacking = false;
    float attackTimePassed = 0;

    public bool IsAttacking { get => isAttacking; }

    // Cached references
    PolygonCollider2D swordCollider;
    SpriteRenderer spriteRenderer;
    PlayerMovement playerMovement;
    Animator animator;

    public float Damage { get => meleeDamage; }

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
        swordCollider = sword.GetComponent<PolygonCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ManageInput();

        if (!playerMovement.IsDashing && canAttack)
        {
            ManageAttack();
        }
    }

    private void ManageAttack()
    {
        attackTimePassed -= Time.deltaTime;
        if (attackTimePassed < 0) // not attacking anymore
        {
            if (Time.time <= meleeAttackTimeInput) // has input for first or next melee attack
            {
                if (!isAttacking)
                {
                    isAttacking = true;
                    animator.SetBool("attacking", true);
                    animator.SetTrigger("melee_attack");

                    animator.SetFloat("m_melee", 1 / meleeAttackDuration);
                    animator.SetFloat("m_ranged", 1 / rangedAttackDuration);

                    attackTimePassed = meleeAttackDuration; // begin first attack
                }
                else // used to play next attack animation
                {
                    animator.SetTrigger("melee_attack");

                    attackTimePassed += meleeAttackDuration; // begin next attack
                }
                
                SwordAttack();
            }
            else if (Time.time <= rangedAttackTimeInput) // has input for first or next ranged attack
            {
                if (!isAttacking)
                {
                    isAttacking = true;
                    animator.SetBool("attacking", true);
                    animator.SetTrigger("ranged_attack");

                    animator.SetFloat("m_melee", 1 / meleeAttackDuration);
                    animator.SetFloat("m_ranged", 1 / rangedAttackDuration);

                    attackTimePassed = rangedAttackDuration; // begin first attack
                }
                else // play ranged attack animation again
                {
                    animator.SetTrigger("ranged_attack");

                    attackTimePassed += rangedAttackDuration; // begin next attack
                }

                PrepareRangedAttack();
            }
            else // no more inputs for attack, stop
            {
                if (isAttacking)
                {
                    isAttacking = false;
                    animator.SetBool("attacking", false);
                }
            }
        }
    }

    private void SwordAttack()
    {
        // Position collider and flip sprite
        if (aimDirection.x >= 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        // Check for enemies hit
        Collider2D[] results = new Collider2D[meleeMaxEnemiesHit];
        swordCollider.OverlapCollider(enemyFilter, results);
        foreach (Collider2D cd in results)
        {
            if (cd != null)
            {
                cd.gameObject.GetComponent<EnemyHealth>().TakeDamage(meleeDamage);
            }
        }
    }

    private void PrepareRangedAttack()
    {
        // Position projectile place and flip sprite
        if (aimDirection.x >= 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    public void ThrowProjectile()
    {
        GameObject proj = Instantiate(projectilePrefab, projectilePlace.transform.position, Quaternion.identity);
        proj.GetComponent<PlayerProjectile>().SetProperties(rangedDamage, aimDirection, projectileSpeed, projectileTravelDistance);
    }

    public void StopAttack(bool stopIt)
    {
        if (stopIt)
        {
            // Reset variables and states
            isAttacking = false;
            animator.SetBool("attacking", false);
            attackTimePassed = 0f;
        }
        canAttack = !stopIt;
    }

    private void ManageInput()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        aimDirection = mousePosition - transform.position;

        if (Input.GetButtonDown("Fire1"))
        {
            meleeAttackTimeInput = Time.time + meleeTimeInputTolerance;
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            rangedAttackTimeInput = Time.time + rangedTimeInputTolerance;
        }
    }
}
