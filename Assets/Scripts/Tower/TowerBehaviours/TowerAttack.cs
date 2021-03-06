using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : TowerBehaviour
{
    [SerializeField] float range;
    [SerializeField] float delayToReloadTime;
    [SerializeField] float reloadTime;
    [SerializeField] GameObject projectilePrefab;

    GameObject currentTarget;
    List<TowerProjectile> projectiles;
    float nextTimeToReload;
    bool charging;
    bool charged;

    override public void DefineBehaviourVariables()
    {
        shootingRange = range;
        shotsPerSec = 1 / (delayToReloadTime + reloadTime);
        projectileDamage = projectilePrefab.GetComponent<TowerProjectile>().Damage;
        projectileSpeed = projectilePrefab.GetComponent<TowerProjectile>().Speed;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentTarget = null;
        projectiles = new List<TowerProjectile>();
        nextTimeToReload = Time.time + delayToReloadTime;
        charging = false;
        charged = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTarget == null || !CharacterList.enemiesAlive.Contains(currentTarget))
        {
            SetNewTarget();
        }

        if (Time.time >= nextTimeToReload && !charged)
        {
            PrepareShot();
        }

        if (charged && currentTarget != null)
        {
            Shoot();
        }

        projectiles.RemoveAll(item => item == null);
    }

    void OnDestroy()
    {
        DestroyProjectileNotShot();
    }

    void SetNewTarget()
    {
        FindNearestEnemyAsTarget();

        if (projectiles.Count > 0)
        {
            for (int i = 0; i < projectiles.Count; i++)
            {
                if ((i == 0 && (charging || charged)) || projectiles[i] == null)
                {
                    continue;
                }
                if (currentTarget == null)
                {
                    projectiles[i].CommandDestruction();
                }
                else
                {
                    projectiles[i].Target = currentTarget;
                }
            }
        }
    }

    void PrepareShot()
    {
        if (!charging)
        {
            charging = true;
            projectiles.Insert(0, Instantiate(projectilePrefab, transform.position + Vector3.up * (transform.localScale.y) * 0.7f, Quaternion.Euler(0, 0, 0)).GetComponent<TowerProjectile>());
        }

        projectiles[0].transform.localScale = Vector3.Lerp(new Vector3(0f, 0f, 1f), projectilePrefab.transform.localScale, (Time.time - nextTimeToReload) / reloadTime);
        
        if (Time.time >= nextTimeToReload + reloadTime)
        {
            charged = true;
            charging = false;
        }
    }

    void Shoot()
    {
        nextTimeToReload = Time.time + delayToReloadTime;
        charged = false;
        projectiles[0].Target = currentTarget;
    }

    void DestroyProjectileNotShot()
    {
        if (charging || charged)
        {
            if (projectiles[0] != null)
            {
                projectiles[0].CommandDestruction();
            }
            projectiles.RemoveAll(item => item == null);
        }
    }

    void FindNearestEnemyAsTarget()
    {
        GameObject nearestEnemy = null;
        float smallestDistance = Mathf.Infinity;

        foreach (GameObject currentEnemy in CharacterList.enemiesAlive)
        {
            float currentDistance = (transform.position - currentEnemy.transform.position).magnitude;
            if (currentDistance < smallestDistance)
            {
                smallestDistance = currentDistance;
                nearestEnemy = currentEnemy;
            }
        }

        if (smallestDistance <= range)
        {
            currentTarget = nearestEnemy;
        }
        else
        {
            currentTarget = null;
        }
    }
}
