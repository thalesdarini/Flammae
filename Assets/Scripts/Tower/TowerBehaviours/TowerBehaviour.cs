using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class TowerBehaviour : MonoBehaviour
{
    protected float shootingRange;
    protected float shotsPerSec;
    protected float projectileDamage;
    protected float projectileSpeed;

    protected float buffingRange;
    protected string buffedStat;
    protected float buffAmount;

    protected float timeToHeal;

    abstract public void DefineBehaviourVariables();

    public float ShootingRange { get => shootingRange; }
    public float ShotsPerSec { get => shotsPerSec; }
    public float ProjectileDamage { get => projectileDamage; }
    public float ProjectileSpeed { get => projectileSpeed; }
    public float BuffingRange { get => buffingRange; }
    public string BuffedStat { get => buffedStat; }
    public float BuffAmount { get => buffAmount; }
    public float TimeToHeal { get => timeToHeal; }
}
