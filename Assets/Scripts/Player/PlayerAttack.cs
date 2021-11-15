using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : AttackScript
{
    [Header("Sword attack")]
    [SerializeField] GameObject sword;
    [SerializeField] float attackTimeInputTolerance;
    [SerializeField] float attackDuration;
    [SerializeField] ContactFilter2D enemyFilter;

    // Input variables
    Vector2 aimDirection;
    float swordAttackTimeInput = -1f; // just below 0 so it doesn't start attacking

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
            attackTimePassed -= Time.deltaTime;

            if (attackTimePassed < 0)
            {
                if (Time.time <= swordAttackTimeInput)
                {
                    isAttacking = true;
                    animator.SetFloat("m_attack", 1 / attackDuration);
                    animator.SetBool("attacking", true);

                    attackTimePassed = attackDuration;
                    SwordAttack();
                }
                else
                {
                    isAttacking = false;
                    animator.SetBool("attacking", false);
                }
            }
        }
        else
        {
            isAttacking = false;
            animator.SetBool("attacking", false);
        }
    }

    private void SwordAttack()
    {
        if (aimDirection.x >= 0)
        {
            sword.transform.localRotation = Quaternion.Euler(0, 0, 0);
            spriteRenderer.flipX = false;
        }
        else
        {
            sword.transform.localRotation = Quaternion.Euler(0, 0, 180);
            spriteRenderer.flipX = true;
        }

        Collider2D[] results = new Collider2D[5];
        swordCollider.OverlapCollider(enemyFilter, results);
        foreach (Collider2D cd in results)
        {
            if (cd != null)
            {
                cd.gameObject.GetComponent<EnemyTest>().TakeDamage(damage);
            }
        }
    }

    public void StopAttack(bool stopIt)
    {
        canAttack = !stopIt;
    }

    private void ManageInput()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        aimDirection = mousePosition - transform.position;

        if (Input.GetButtonDown("Fire1"))
        {
            swordAttackTimeInput = Time.time + attackTimeInputTolerance;
        }
    }
}
