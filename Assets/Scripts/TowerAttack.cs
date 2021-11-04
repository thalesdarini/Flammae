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
    List<GameObject> projectiles;
    float nextTimeToReload;
    bool charging;
    bool charged;

    override public void DefineBehaviourVariables()
    {
        behaviour = "attack";
        shootingRange = range;
        shotsPerSec = 1 / (delayToReloadTime + reloadTime);
        projectileDamage = projectilePrefab.GetComponent<Projectile>().Damage;
        projectileSpeed = projectilePrefab.GetComponent<Projectile>().Speed;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentTarget = null;
        projectiles = new List<GameObject>();
        nextTimeToReload = Time.time;
        charging = false;
        charged = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTarget == null)
        {
            SetNewTarget();
        }

        if(Time.time >= nextTimeToReload && !charged)
        {
            PrepareShot();
        }

        if(charged && currentTarget != null)
        {
            Shoot();
        }

        projectiles.RemoveAll(item => item == null);
    }

    private void OnDestroy()
    {
        if(charging || charged)
        {
            Destroy(projectiles[0]);
            projectiles.RemoveAll(item => item == null);
        }
    }

    void PrepareShot()
    {
        if(!charging)
        {
            projectiles.Insert(0, Instantiate(projectilePrefab, transform.position + Vector3.up * (transform.localScale.y) / 3, Quaternion.Euler(0, 0, 0)));
            charging = true;
        }

        float scaleFunctionX = projectilePrefab.transform.localScale.x * (Time.time - nextTimeToReload) / reloadTime;
        float scaleFunctionY = projectilePrefab.transform.localScale.y * (Time.time - nextTimeToReload) / reloadTime;
        projectiles[0].transform.localScale = new Vector2(scaleFunctionX, scaleFunctionY);
        
        if(Time.time >= nextTimeToReload + reloadTime)
        {
            charging = false;
            charged = true;
        }
    }

    void Shoot()
    {
        projectiles[0].GetComponent<Projectile>().Target = currentTarget;
        nextTimeToReload = Time.time + delayToReloadTime;
        charged = false;
    }

    void SetNewTarget()
    {
        GameObject nearestEnemy = null;
        float smallestDistance = Mathf.Infinity;

        foreach(GameObject currentEnemy in CharacterList.enemiesAlive)
        {
            float currentDistance = (transform.position - currentEnemy.transform.position).magnitude;
            if(currentDistance < smallestDistance)
            {
                smallestDistance = currentDistance;
                nearestEnemy = currentEnemy;
            }
        }

        if(smallestDistance <= range)
        {
            currentTarget = nearestEnemy;
        }
        else
        {
            currentTarget = null;
        }

        if(projectiles.Count > 0)
        {
            for(int i=0; i<projectiles.Count; i++)
            {
                if((i == 0 && (charging || charged)) || projectiles[i] == null)
                {
                    continue;
                }
                if (currentTarget == null)
                {
                    Destroy(projectiles[i]);
                }
                else
                {
                    projectiles[i].GetComponent<Projectile>().Target = currentTarget;
                }
            }
        }
    }
}
