using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherArrow : MonoBehaviour
{
    private float damage;
    private Vector2 direction;
    private float speed;

    private float duration;
    private bool propertiesSet = false;

    [SerializeField] LayerMask playerLayerMask;
    [SerializeField] LayerMask alliesLayerMask;

    public void SetProperties(float damage, Vector2 direction, float speed, float travelDistance)
    {
        this.damage = damage;
        this.direction = direction.normalized;
        this.speed = speed;

        duration = travelDistance / speed;
        propertiesSet = true;

        transform.rotation = Quaternion.Euler(Vector3.forward * Vector3.SignedAngle(Vector3.right, (Vector3) direction, Vector3.forward));
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
        // Convert the object's layer to a bitfield for comparison
        int objLayerMask = 1 << collision.gameObject.layer;

        if ((playerLayerMask.value & objLayerMask) > 0 || (alliesLayerMask.value & objLayerMask) > 0) // collided with enemy
        {
            collision.gameObject.GetComponent<HealthScript>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}