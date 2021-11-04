using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] float speed;

    GameObject target;
    Animator animatorReference;
    bool animationFired;

    public GameObject Target { set => target = value; }
    public float Damage { get => damage; }
    public float Speed { get => speed; }

    // Start is called before the first frame update
    void Start()
    {
        target = null;
        animatorReference = transform.Find("Animation").GetComponent<Animator>();
        animationFired = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            if (!animationFired)
            {
                animatorReference.SetTrigger("Shoot");
            }
            transform.Translate((target.transform.position - transform.position).normalized * speed * Time.deltaTime, Space.World);
            transform.localEulerAngles = Vector3.forward * Vector2.SignedAngle(Vector2.up, target.transform.position - transform.position);
        }
    }

    void OnTriggerEnter2D(Collider2D objectThatEntered)
    {
        if (objectThatEntered.gameObject == target)
        {
            objectThatEntered.GetComponent<EnemyTest>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
