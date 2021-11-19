using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHeal : TowerBehaviour
{
    [SerializeField] float healingPercentagePerSecond;
    [SerializeField] float range;
    [SerializeField] GameObject healIndicatorParticlePrefab;
    [SerializeField] GameObject fullHealthIndicatorParticlePrefab;

    PlayerHealth playerBeingHealed;

    override public void DefineBehaviourVariables()
    {
        timeToHeal = 1f / healingPercentagePerSecond;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerBeingHealed = null;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealState();

        if (playerBeingHealed != null)
        {
            playerBeingHealed.Heal(healingPercentagePerSecond * Time.deltaTime);
        }
    }

    void OnDestroy()
    {
        RemoveHealState();
    }

    void UpdateHealState()
    {
        foreach (GameObject currentPlayer in CharacterList.playersAlive)
        {
            PlayerHealth currentPlayerHealthScript = currentPlayer.GetComponent<PlayerHealth>();
            bool inRange = (transform.position - currentPlayer.transform.position).magnitude <= range;

            if (inRange && playerBeingHealed == null && !currentPlayerHealthScript.IsHealing && currentPlayerHealthScript.HealthPercentual < 1f)
            {
                SetHealStateAndIndicator(currentPlayerHealthScript);
            }
            else if ((playerBeingHealed == currentPlayerHealthScript) && (!inRange || currentPlayerHealthScript.HealthPercentual >= 1f))
            {
                ResetHealStateAndIndicator(currentPlayerHealthScript);
            }
        }
    }

    void RemoveHealState()
    {
        foreach (GameObject currentPlayer in CharacterList.playersAlive)
        {
            PlayerHealth currentPlayerHealthScript = currentPlayer.GetComponent<PlayerHealth>();

            if (playerBeingHealed == currentPlayerHealthScript)
            {
                ResetHealStateAndIndicator(currentPlayerHealthScript);
            }
        }
    }

    void SetHealStateAndIndicator(PlayerHealth player)
    {
        GameObject indicatorParticle = Instantiate(healIndicatorParticlePrefab, player.transform);
        indicatorParticle.transform.localPosition += Vector3.down * 0.01f;

        playerBeingHealed = player;
        player.IsHealing = true;
    }

    void ResetHealStateAndIndicator(PlayerHealth player)
    {
        playerBeingHealed = null;
        player.IsHealing = false;

        Destroy(player.transform.Find("HealIndicator(Clone)").gameObject);
        if (player.HealthPercentual >= 1f)
        {
            GameObject indicatorParticle = Instantiate(fullHealthIndicatorParticlePrefab, player.transform);
            indicatorParticle.transform.localPosition += Vector3.down * 0.01f;
        }
    }
}
