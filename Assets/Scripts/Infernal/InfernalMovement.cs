using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfernalMovement : MovementScript
{
    [Header("Find Enemy")]
    [SerializeField] ContactFilter2D enemyLayer;
    [SerializeField] float findRange;
    [SerializeField] float closestDistanceToEnemy;
    float findTickRate = 0.4f;

    // current state variables
    float findTimeCounter = 0f;
    GameObject enemyFound = null;

    // cached references
    InfernalAttack infernalAttack;

    override protected void Start()
    {
        base.Start();
        infernalAttack = GetComponent<InfernalAttack>();
    }

    private void Update()
    {
        if (enemyFound == null)
        {
            if(!infernalAttack.IsAttacking)
                LookForEnemy();
        }
        else
        {
            WalkToEnemy();
        }
    }

    private void LookForEnemy()
    {
        findTimeCounter += Time.deltaTime;
        if (findTimeCounter > findTickRate)
        {
            findTimeCounter = 0f;

            Collider2D[] results = new Collider2D[5];
            int n = Physics2D.OverlapCircle(transform.position, findRange, enemyLayer, results);
            if (n != 0)
            {
                int closest = 0;
                float closestDistance = Mathf.Infinity;
                for (int i = 0; i < n; i++)
                {
                    if (results[i] != null)
                    {
                        float aux = Vector2.Distance(transform.position, results[i].transform.position);
                        if (aux < closestDistance)
                        {
                            closest = i;
                            closestDistance = aux;
                        }
                    }
                }
                enemyFound = results[closest].gameObject;
            }
        }
    }

    private void WalkToEnemy()
    {
        Vector2 currentPosition = transform.position;
        Vector2 targetPosition = enemyFound.transform.position;

        if (Vector2.Distance(currentPosition, targetPosition) > closestDistanceToEnemy)
            transform.position = Vector2.MoveTowards(currentPosition, targetPosition, movementSpeed * Time.deltaTime);
        else if (!infernalAttack.IsAttacking)
            infernalAttack.StartAttacking(enemyFound);

        // rotate
        if (targetPosition.x > currentPosition.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }
    }
}
