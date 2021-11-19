using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MovementScript
{
    [Header("Dash")]
    [SerializeField] float dashSpeed;
    [SerializeField] float dashDistance;
    [SerializeField] float dashCooldown;

    // Input variables
    Vector2 moveDirection;
    Vector2 facingDirection = new Vector2(0, -1);
    bool dashKey = false;

    // Current state variables
    bool canMove = true;
    bool dashAvailable = true;
    bool isDashing = false;
    Vector2 dashDirection;

    public bool IsDashing { get => isDashing; }

    // Cached references
    Rigidbody2D rb2D;
    PlayerAttack playerAttack;
    Animator animator;
    SpriteRenderer spriteRenderer;

    public float MovementSpeed { get => movementSpeed; }

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
        rb2D = GetComponent<Rigidbody2D>();
        playerAttack = GetComponent<PlayerAttack>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        ManageInput();
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            if (dashAvailable && dashKey && !playerAttack.IsAttacking)
            {
                StartCoroutine(StartDash());
            }

            if (isDashing)
            {
                Dash();
            }
            else
            {
                Move();
            }

            if (moveDirection != Vector2.zero)
                animator.SetBool("running", true);
            else
                animator.SetBool("running", false);

            if (!playerAttack.IsAttacking)
                FlipSprite();
        }
        else
        {
            animator.SetBool("running", false);
            animator.SetBool("isDashing", false);
            rb2D.velocity = Vector2.zero;
        }

        // reset key even if not used
        dashKey = false;
    }

    private void FlipSprite()
    {
        if (moveDirection.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (moveDirection.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    IEnumerator StartDash()
    {
        dashAvailable = false;
        isDashing = true;
        dashDirection = facingDirection;
        animator.SetBool("isDashing", true);

        //Let it be dashing until duration ends
        float dashDuration = dashDistance / dashSpeed;
        animator.SetFloat("m_dash", 1 / dashDuration);
        
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        animator.SetBool("isDashing", false);

        //Recharge dash after cooldown time has passed
        yield return new WaitForSeconds(dashCooldown - dashDuration);
        dashAvailable = true;
    }

    void Dash()
    {
        rb2D.velocity = dashDirection * dashSpeed;
    }

    void Move()
    {
        rb2D.velocity = moveDirection * movementSpeed;
    }

    public void PauseMovement(bool pauseIt)
    {
        canMove = !pauseIt;
    }

    void ManageInput()
    {
        //Movement
        float xAxisMove = Input.GetAxisRaw("Horizontal");
        float yAxisMove = Input.GetAxisRaw("Vertical");

        //Controller joystick snap
        xAxisMove = (xAxisMove > 0.1f) ? 1 : (xAxisMove < -0.1f) ? -1 : 0;
        yAxisMove = (yAxisMove > 0.1f) ? 1 : (yAxisMove < -0.1f) ? -1 : 0;

        moveDirection = new Vector2(xAxisMove, yAxisMove).normalized;

        // keep track of last direction moved for its use in dash ability
        facingDirection = (moveDirection == Vector2.zero) ? facingDirection : moveDirection;

        //Dash
        if (Input.GetButtonDown("Jump"))
        {
            dashKey = true;
        }
    }
}
