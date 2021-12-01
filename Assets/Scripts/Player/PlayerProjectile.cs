using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private float damage;
    private Vector2 direction;
    private float speed;

    private float duration;
    private bool propertiesSet = false;

    [SerializeField] LayerMask enemyLayerMask;

    public void SetProperties(float damage, Vector2 direction, float speed, float travelDistance)
    {
        this.damage = damage;
        this.direction = direction.normalized;
        this.speed = speed;

        duration = travelDistance / speed;
        propertiesSet = true;
    }

    private void Start()
    {
        SoundManager.instance.PlaySoundEffect(SoundManager.cast, .4f);
    }

    void Update()
    {
        if (propertiesSet)
        {
            duration -= Time.deltaTime;
            if (duration < 0)
            {
                Destroy(gameObject);
            }
            else
            {
                transform.position += (Vector3) direction * speed * Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.isTrigger)
        {
            // Convert the object's layer to a bitfield for comparison
            int objLayerMask = 1 << collision.gameObject.layer;

            if ((enemyLayerMask.value & objLayerMask) > 0) // collided with enemy
            {
                collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}
