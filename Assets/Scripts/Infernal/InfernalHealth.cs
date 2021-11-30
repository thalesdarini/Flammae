using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfernalHealth : HealthScript
{
    private float healthPercentual = 1f;

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

        if (healthPercentual <= 0)
        {
            //StartCoroutine(Die());
        }
    }
}
