using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyTest : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] float attack;
    [SerializeField] float speed;
    [SerializeField] int soulDrop;
    [SerializeField] float soulDropRange;
    [SerializeField] GameObject soulPrefab;

    AIDestinationSetter enemyDestination;

    void Awake()
    {
        CharacterList.enemiesAlive.Add(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyDestination = GetComponent<AIDestinationSetter>();
        enemyDestination.target = GameObject.Find("Waypoint").transform;
    }

    void OnDestroy()
    {
        CharacterList.enemiesAlive.Remove(gameObject);
    }

    public void TakeDamage(float amountOfDamage)
    {
        health -= amountOfDamage;
        GetComponent<Animator>().SetTrigger("Damage");
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        for (int i = 0; i < soulDrop; i++)
        {
            Vector3 randomDisplacement = new Vector3(Random.Range(-soulDropRange, soulDropRange), Random.Range(-soulDropRange / 2, soulDropRange / 2), 0);
            Instantiate(soulPrefab, transform.position + randomDisplacement, Quaternion.Euler(0, 0, 0));
        }
        Destroy(gameObject);
    }
}
