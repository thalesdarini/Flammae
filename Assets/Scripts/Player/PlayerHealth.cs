using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HealthScript
{
    float healthPercentual;

    public float MaxHealth { get => maxHealth; }
    public float HealthPercentual { get => healthPercentual; }

    void Awake()
    {
        CharacterList.alliesAlive.Add(gameObject);
    }

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
        healthPercentual = 1;
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
            Die();
        }
    }

    void Die()
    {
        GetComponent<PlayerSoulCounter>().LoseSouls(GetComponent<PlayerSoulCounter>().NumSouls / 2);
        Respawn();
    }

    void Respawn()
    {
        //tira de um lugar e joga em outro sla
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
