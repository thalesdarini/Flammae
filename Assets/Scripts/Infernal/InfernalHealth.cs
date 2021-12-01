using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfernalHealth : HealthScript
{
    private float healthPercentual = 1f;
    private float deadDuration = 1f;
    private bool dead = false;

    public bool Dead { get => dead; }

    void Awake()
    {
        CharacterList.alliesAlive.Add(gameObject);
    }

    private void OnDestroy()
    {
        CharacterList.alliesAlive.Remove(gameObject);
    }

    override protected void Start()
    {
        base.Start();
    }

    public override void TakeDamage(float amountOfDamage)
    {
        healthPercentual -= amountOfDamage / maxHealth;

        if (healthPercentual <= 0 && !dead)
        {
            Die();
        }
    }

    private void Die()
    {
        GetComponent<Animator>().SetTrigger("die");
        dead = true;
        GetComponent<Rigidbody2D>().simulated = false;
        SoundManager.instance.PlaySoundEffect(SoundManager.infernal_fall, 1f, transform.position);
        Destroy(gameObject, deadDuration);
    }
}
