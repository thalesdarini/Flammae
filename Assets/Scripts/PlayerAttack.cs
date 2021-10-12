using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject sword;
    [SerializeField] ContactFilter2D cf2d;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] float damage;
    PolygonCollider2D swordCollider;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
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

    private void FlipPlayer(float angle)
    {
        if (angle <= 135 && angle > 45) // right
        {
            spriteRenderer.sprite = sprites[0];
        }
        else if (angle <= 45 && angle > -45) // down
        {
            spriteRenderer.sprite = sprites[1];
        }
        else if (angle <= -45 && angle > -135 ) // left
        {
            spriteRenderer.sprite = sprites[2];
        }
        else // up
        {
            spriteRenderer.sprite = sprites[3];
        }
    }
}
