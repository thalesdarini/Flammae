using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuff : TowerBehaviour
{
    [SerializeField] string whichStat;
    [SerializeField] float buffingAmount;
    [SerializeField] float range;
    [SerializeField] float delayBetweenPulses;
    [SerializeField] GameObject indicatorParticlePrefab;
    [SerializeField] GameObject pulsePrefab;

    List<GameObject> alliesBuffedByThisTower;
    GameObject pulseWave;
    float nextTimeToPulse;

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
        pulseWave = null;
        nextTimeToPulse = Time.time + delayBetweenPulses;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextTimeToPulse)
        {
            Pulse();
        }
    }

    void OnDestroy()
    {
        Destroy(pulseWave);
        RemoveAllBuffs();
    }

    void Pulse()
    {
        if (pulseWave == null)
        {
            pulseWave = Instantiate(pulsePrefab, transform.position + Vector3.up * (transform.localScale.y) * 0.5f, Quaternion.Euler(0, 0, 0));
        }

        pulseWave.transform.localScale = Vector3.Lerp(new Vector3(0f, 0f, 1f), new Vector3(2 * range, 2 * range, 1f), (Time.time - nextTimeToPulse) / (delayBetweenPulses * 0.25f));

        if (Time.time >= nextTimeToPulse + delayBetweenPulses * 0.25f)
        {
            nextTimeToPulse = Time.time + delayBetweenPulses * 0.75f;
            Destroy(pulseWave);
            UpdateBuffs();
        }
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

    void RemoveAllBuffs()
    {
        foreach (GameObject currentAlly in CharacterList.alliesAlive)
        {
            bool alreadyBuffedByThisTower = alliesBuffedByThisTower.Contains(currentAlly);

            if (alreadyBuffedByThisTower)
            {
                ResetBuffAndIndicator(currentAlly);
            }
        }
    }

    void SetBuffAndIndicator(GameObject ally)
    {
        if (!CheckBuffed(ally))
        {
            GameObject indicatorParticle = Instantiate(indicatorParticlePrefab, ally.transform);
            indicatorParticle.transform.localPosition += Vector3.down * 0.51f;
        }

        ApplyBuff(ally);
        alliesBuffedByThisTower.Add(ally);
    }

    void ResetBuffAndIndicator(GameObject ally)
    {
        RemoveBuff(ally);
        alliesBuffedByThisTower.Remove(ally);

        if (!CheckBuffed(ally))
        {
            if (whichStat == "Ataque")
            {
                Destroy(ally.transform.Find("AttackBuffIndicator(Clone)").gameObject);
            }
            else if (whichStat == "Velocidade")
            {
                Destroy(ally.transform.Find("SpeedBuffIndicator(Clone)").gameObject);
            }
            else if (whichStat == "Vida")
            {
                Destroy(ally.transform.Find("HealthBuffIndicator(Clone)").gameObject);
            }
        }
    }

    void ApplyBuff(GameObject ally)
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
            return false; //NAO VAI ACONTECER (ESPERO)
        }
    }
}
