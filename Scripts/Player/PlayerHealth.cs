using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HealthScript
{
    float healthPercentual;
    bool isHealing;

    public float MaxHealth { get => maxHealth; }
    public float HealthPercentual { get => healthPercentual; }
    public bool IsHealing { get => isHealing; set => isHealing = value; }

    void Awake()
    {
        CharacterList.alliesAlive.Add(gameObject);
        CharacterList.playersAlive.Add(gameObject);
    }

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
        healthPercentual = 0.2f;
    }

    void OnDestroy()
    {
        CharacterList.alliesAlive.Remove(gameObject);
        CharacterList.playersAlive.Remove(gameObject);
    }

    override public void TakeDamage(float amountOfDamage)
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
