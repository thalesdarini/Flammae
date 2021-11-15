using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HealthScript
{
    [Header("Respawn")]
    [SerializeField] Transform respawnPosition;
    [SerializeField] float respawnTime;

    float healthPercentual;

    public float MaxHealth { get => maxHealth; }
    public float HealthPercentual { get => healthPercentual; }

    // Cached references
    PlayerMovement playerMovement;
    PlayerAttack playerAttack;
    Animator animator;
    Rigidbody2D rb2D;

    void Awake()
    {
        CharacterList.alliesAlive.Add(gameObject);
    }

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
        healthPercentual = 1;

        playerMovement = GetComponent<PlayerMovement>();
        playerAttack = GetComponent<PlayerAttack>();
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    float deathTime = 15f;
    private void Update()
    {
        if (Time.time > deathTime)
        {
            StartCoroutine(Die());
            deathTime += 20f;
        }
    }

    void OnDestroy()
    {
        CharacterList.alliesAlive.Remove(gameObject);
    }

    public void TakeDamage(float amountOfDamage)
    {
        GetComponent<Animator>().SetTrigger("Damage");

        healthPercentual -= amountOfDamage / maxHealth;

        if (healthPercentual <= 0)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        rb2D.simulated = false;
        animator.SetBool("dead", true);
        playerMovement.PauseMovement(true);
        playerAttack.StopAttack(true);
        GetComponent<PlayerSoulCounter>().LoseSouls(GetComponent<PlayerSoulCounter>().NumSouls / 2);

        yield return new WaitForSeconds(respawnTime);
        Respawn();
    }

    void Respawn()
    {
        // reset everything
        healthPercentual = 1;
        playerMovement.PauseMovement(false);
        playerAttack.StopAttack(false);
        animator.SetBool("dead", false);
        rb2D.simulated = true;

        // teleport
        transform.position = respawnPosition.position;
    }

    public void Heal(float healPercentage)
    {
        if (healthPercentual + healPercentage >= 1)
        {
            healthPercentual = 1;
        }
        else
        {
            healthPercentual += healPercentage;
        }
    }
}
