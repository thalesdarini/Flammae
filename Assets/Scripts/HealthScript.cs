using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class HealthScript : MonoBehaviour
{
    [Header("Life")]
    [SerializeField] float defaultMaxHealth;
    [SerializeField] protected Sprite[] deathSprites;

    protected float maxHealth;
    bool isBuffed;

    public bool IsBuffed { get => isBuffed; }

    // Start is called before the first frame update
    virtual protected void Start()
    {
        maxHealth = defaultMaxHealth;
        isBuffed = false;
    }

    public void MaxHealthModify(float buff)
    {
        maxHealth += buff;

        if (maxHealth == defaultMaxHealth)
        {
            isBuffed = false;
        }
        else
        {
            isBuffed = true;
        }
    }
}
