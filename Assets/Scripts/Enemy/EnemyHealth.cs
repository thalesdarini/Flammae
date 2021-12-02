using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float maxHealth;
    [SerializeField] int soulDrop;
    [SerializeField] float soulDropRange;
    [SerializeField] GameObject soulPrefab;
    [SerializeField] float deathTime;
    [SerializeField] float stunTimer;

    Rigidbody2D rb2d;
    Animator enemyLifeAnimation;
    EnemyMovement enemyMovement;
    EnemyAttack enemyAttack;
    Slider healthBar;
    float currentHealth;
    float stunTimeRemaining = 0.0f;
    bool isKilled;

    public bool IsKilled { get => isKilled; }

    void Awake()
    {
        CharacterList.enemiesAlive.Add(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        enemyLifeAnimation = GetComponent<Animator>();
        enemyLifeAnimation.SetFloat("dieMult", 1 / deathTime);
        enemyLifeAnimation.SetFloat("stunMult", 1 / stunTimer);
        enemyMovement = GetComponent<EnemyMovement>();
        enemyAttack = GetComponent<EnemyAttack>();
        healthBar = transform.Find("HealthCanvas").Find("HealthBar").GetComponent<Slider>();
        currentHealth = maxHealth;
        healthBar.value = 1f;
        isKilled = false;
    }
    
    // Update is called once per frame
    void Update(){
        if(isKilled == false && stunTimeRemaining > 0){
            stunTimeRemaining -= Time.deltaTime;
            if(stunTimeRemaining <= 0){
                enemyMovement.CanMove = true;
                enemyLifeAnimation.SetBool("takeDamage", false);
            }
        }
    }

    void OnDestroy()
    {
        if (CharacterList.enemiesAlive.Contains(gameObject))
        {
            CharacterList.enemiesAlive.Remove(gameObject);
        }
    }

    //Deducts amout of damage to health - Upon reaching zero, dies
    //Damage animation is played
    public void TakeDamage(float amountOfDamage)
    {
        if (isKilled == false)
        {
            currentHealth -= amountOfDamage;
            rb2d.velocity = Vector2.zero;
            enemyMovement.CanMove = false;
            enemyAttack.IsAttacking = false;
            stunTimeRemaining = stunTimer;

            enemyLifeAnimation.SetBool("takeDamage", true);
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Die();
            }
            else
            {
                enemyLifeAnimation.SetTrigger("takeDamage");
            }

            healthBar.value = currentHealth / maxHealth;
        }
    }

    //Die, drops souls, destroy object...
    void Die()
    {
        isKilled = true;
        CharacterList.enemiesAlive.Remove(gameObject);

        for (int i = 0; i < soulDrop; i++)
        {
            Vector3 randomDisplacement = new Vector3(Random.Range(-soulDropRange, soulDropRange), Random.Range(-soulDropRange / 2, soulDropRange / 2), 0);
            Instantiate(soulPrefab, transform.position + randomDisplacement, Quaternion.Euler(0, 0, 0));
        }
        enemyLifeAnimation.SetTrigger("dies");
        enemyLifeAnimation.SetBool("takeDamage", false);
        enemyLifeAnimation.SetBool("isMoving", false);
        rb2d.simulated = false;

        Destroy(gameObject, deathTime);
    }
}
