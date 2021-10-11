using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;

    [Header("Dash")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDistance;
    [SerializeField] private float dashCooldown;

    // Input variables
    private Vector2 moveDirection;
    private Vector2 facingDirection = new Vector2(0, -1);
    private bool dashKey = false;

    // Current state variables
    private bool canMove = true;
    private bool dashAvailable = true;
    private bool isDashing = false;
    private Vector2 dashDirection;

    // Cached references
    Rigidbody2D rb2D;

    private void Awake()
    {
        // Get components
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        ManageInput();
    }

    private void FixedUpdate()
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

    private IEnumerator StartDash()
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

    private void Dash()
    {
        rb2D.velocity = dashDirection * dashSpeed;
    }

    private void Move()
    {
        rb2D.velocity = moveDirection * moveSpeed;
    }

    public void PauseMovement(bool pauseIt)
    {
        canMove = !pauseIt;
    }

    private void ManageInput()
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

    public Vector2 GetFacingDirection()
    {
        return facingDirection;
    }
}
