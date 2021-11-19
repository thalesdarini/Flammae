using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : AttackScript
{
    [SerializeField] GameObject sword;
    [SerializeField] ContactFilter2D cf2d;

    PolygonCollider2D swordCollider;
    SpriteRenderer spriteRenderer;

    public float Damage { get => damage; }

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
        swordCollider = sword.GetComponent<PolygonCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 aimDirection = mousePosition - transform.position;
        float angle = Vector2.SignedAngle(Vector2.down, aimDirection);
        sword.transform.localRotation = Quaternion.Euler(0, 0, angle);
        
        FlipPlayer(angle);

        if (Input.GetButtonDown("Fire1"))
        {
            Collider2D[] results = new Collider2D[5];
            swordCollider.OverlapCollider(cf2d, results);
            foreach (Collider2D cd in results)
            {
                if (cd != null)
                {
                    cd.gameObject.GetComponent<EnemyTest>().TakeDamage(damage);
                }
            }

            sword.GetComponent<Animator>().SetTrigger("Attack");
        }
    }

    void FlipPlayer(float angle)
    {
        if (angle <= 135 && angle > 45) // right
        {
            spriteRenderer.sprite = attackSprites[0];
        }

        // Check for enemies hit
        Collider2D[] results = new Collider2D[meleeMaxEnemiesHit];
        swordCollider.OverlapCollider(enemyFilter, results);
        foreach (Collider2D cd in results)
        {
            if (cd != null)
            {
                cd.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
            }
        }
    }

    public void StopAttack(bool stopIt)
    {
        if (stopIt)
        {
            spriteRenderer.sprite = attackSprites[2];
        }
        else // up
        {
            spriteRenderer.sprite = attackSprites[3];
        }
    }
}
