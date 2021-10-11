using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] float speed;

    GameObject target;
    public GameObject Target { set => target = value; }

    // Start is called before the first frame update
    void Start()
    {
        target = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            transform.Translate((target.transform.position - transform.position).normalized * speed * Time.deltaTime);
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
