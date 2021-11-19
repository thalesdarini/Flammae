using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] int soulDrop;
    [SerializeField] float soulDropRange;
    [SerializeField] GameObject soulPrefab;

    Animator enemyLifeAnimation;
    EnemyMovement enemyMovement;
    float stunTimer = 0.0f;
    float deathTime = 1.0f;
    float maxSpeed;
    bool isKilled;

    void Awake()
    {
        CharacterList.enemiesAlive.Add(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyLifeAnimation = GetComponent<Animator>();
        enemyMovement = GetComponent<EnemyMovement>();
        isKilled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isKilled == false && stunTimer > 0){
            stunTimer -= Time.deltaTime;
            if(stunTimer <= 0){
                enemyMovement.Speed = maxSpeed;
            }
        }
        if(isKilled == true){
            deathTime -= Time.deltaTime;
            if(deathTime <= 0){
                Destroy(gameObject);
            }
        }
    }

    void OnDestroy()
    {
        CharacterList.enemiesAlive.Remove(gameObject);
    }

    //Deducts amout of damage to health - Upon reaching zero, dies
    //Damage animation is played
    public void TakeDamage(float amountOfDamage)
    {
        if(isKilled == false){
            health -= amountOfDamage;
            maxSpeed = enemyMovement.Speed;
            enemyMovement.Speed = 0.01f;
            stunTimer = 0.4f;

            if (health <= 0)
            {
                Die();
            }
            else
            {
                enemyLifeAnimation.SetTrigger("takeDamage");
            }
        }
    }

    //Die, drops souls, destroy object...
    void Die()
    {
        //TODO: Enemy Die Animation
        for (int i = 0; i < soulDrop; i++)
        {
            Vector3 randomDisplacement = new Vector3(Random.Range(-soulDropRange, soulDropRange), Random.Range(-soulDropRange / 2, soulDropRange / 2), 0);
            Instantiate(soulPrefab, transform.position + randomDisplacement, Quaternion.Euler(0, 0, 0));
        }
        enemyLifeAnimation.SetTrigger("dies");
        isKilled = true;
    }
}
