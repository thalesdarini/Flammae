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

    // Cached references
    Rigidbody2D rb2D;
    SpriteRenderer spriteRenderer;

    public float MovementSpeed { get => movementSpeed; }

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        ManageInput();
        //FlipPlayer();
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            if (dashAvailable && dashKey)
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
        }

        // reset key even if not used
        dashKey = false;
    }

    IEnumerator StartDash()
    {
        dashAvailable = false;
        isDashing = true;
        dashDirection = facingDirection;

        //Let it be dashing until duration ends
        float dashDuration = dashDistance / dashSpeed;
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;

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

        //Controller joystick deadzone
        xAxisMove = (xAxisMove > 0.19f) ? 1 : (xAxisMove < -0.19f) ? -1 : 0;
        yAxisMove = (yAxisMove > 0.19f) ? 1 : (yAxisMove < -0.19f) ? -1 : 0;

        moveDirection = new Vector2(xAxisMove, yAxisMove).normalized;

        // keep track of last direction moved for its use in dash ability
        facingDirection = (moveDirection == Vector2.zero) ? facingDirection : moveDirection;

        //Dash
        if (Input.GetButtonDown("Jump"))
        {
            dashKey = true;
        }
    }

    void FlipPlayer()
    {
        if (facingDirection.x > 0)
        {
            spriteRenderer.sprite = walkSprites[1];
        }
        else if (facingDirection.x < 0)
        {
            spriteRenderer.sprite = walkSprites[3];
        }
        else if (facingDirection.y < 0)
        {
            spriteRenderer.sprite = walkSprites[0];
        }
        else if (facingDirection.y > 0)
        {
            spriteRenderer.sprite = walkSprites[2];
        }
    }
}
