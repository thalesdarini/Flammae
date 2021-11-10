using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuff : TowerBehaviour
{
    [SerializeField] float range;
    [SerializeField] string whichStat;
    [SerializeField] float buffingAmount;
    [SerializeField] GameObject indicatorParticlePrefab;

    List<GameObject> alliesBuffedByThisTower;

    override public void DefineBehaviourVariables()
    {
        behaviour = "buff";
        buffingRange = range;
        buffedStat = whichStat;
        buffAmount = buffingAmount;
    }

    // Start is called before the first frame update
    void Start()
    {
        alliesBuffedByThisTower = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBuffs();
    }

    void UpdateBuffs()
    {
        foreach (GameObject currentAlly in CharacterList.alliesAlive)
        {
            bool inRange = (transform.position - currentAlly.transform.position).magnitude <= range;
            bool alreadyBuffedByThisTower = alliesBuffedByThisTower.Contains(currentAlly);

            if (inRange && !alreadyBuffedByThisTower)
            {
                SetBuffAndIndicator(currentAlly);
            }
            else if (!inRange && alreadyBuffedByThisTower)
            {
                ResetBuffAndIndicator(currentAlly);
            }
        }
    }

    void SetBuffAndIndicator(GameObject ally)
    {
        if (!CheckBuffed(ally))
        {
            Instantiate(indicatorParticlePrefab, ally.transform);
        }

        Buff(ally);
    }

    void ResetBuffAndIndicator(GameObject ally)
    {
        RemoveBuff(ally);

        if (!CheckBuffed(ally))
        {
            if (whichStat == "Ataque")
            {
                Destroy(ally.transform.Find("AttackBuffIndicator").gameObject);
            }
            else if (whichStat == "Velocidade")
            {
                Destroy(ally.transform.Find("SpeedBuffIndicator").gameObject);
            }
            else if (whichStat == "Vida")
            {
                Destroy(ally.transform.Find("HealthBuffIndicator").gameObject);
            }
        }
    }

    void Buff(GameObject ally)
    {
        if (whichStat == "Ataque")
        {
            ally.GetComponent<AttackScript>().AttackModify(buffingAmount);
        }
        else if (whichStat == "Velocidade")
        {
            ally.GetComponent<MovementScript>().SpeedModify(buffingAmount);
        }
        else if (whichStat == "Vida")
        {
            ally.GetComponent<HealthScript>().MaxHealthModify(buffingAmount);
        }
    }

    void RemoveBuff(GameObject ally)
    {
        if (whichStat == "Ataque")
        {
            ally.GetComponent<AttackScript>().AttackModify(-buffingAmount);
        }
        else if (whichStat == "Velocidade")
        {
            ally.GetComponent<MovementScript>().SpeedModify(-buffingAmount);
        }
        else if (whichStat == "Vida")
        {
            ally.GetComponent<HealthScript>().MaxHealthModify(-buffingAmount);
        }
    }

    bool CheckBuffed(GameObject ally)
    {
        if (whichStat == "Ataque")
        {
            return ally.GetComponent<AttackScript>().IsBuffed;
        }
        else if (whichStat == "Velocidade")
        {
            return ally.GetComponent<MovementScript>().IsBuffed;
        }
        else if (whichStat == "Vida")
        {
            return ally.GetComponent<HealthScript>().IsBuffed;
        }
        else
        {
            return false;
        }
    }
}
