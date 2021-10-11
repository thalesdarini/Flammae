using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] float attack;
    [SerializeField] float speed;
    [SerializeField] int soulDrop;
    [SerializeField] float soulDropRange;
    [SerializeField] GameObject soulPrefab;

    void Awake()
    {
        EnemyList.enemiesAlive.Add(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Die()
    {
        EnemyList.enemiesAlive.Remove(gameObject);
        for(int i=0; i<soulDrop; i++)
        {
            Vector3 randomDisplacement = new Vector3(Random.Range(-soulDropRange, soulDropRange), Random.Range(-soulDropRange/2, soulDropRange/2), 0);
            Instantiate(soulPrefab, transform.position + randomDisplacement, Quaternion.Euler(0, 0, 0));
        }
        Destroy(gameObject);
    }

    public void TakeDamage(float amountOfDamage)
    {
        health -= amountOfDamage;
        if(health <= 0)
        {
            Die();
        }
    }
}