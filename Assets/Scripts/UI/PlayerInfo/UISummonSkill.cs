using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISummonSkill : MonoBehaviour
{
    [SerializeField] GameObject fade;
    [SerializeField] Text cooldownText;
    [SerializeField] Text soulCost;
    [SerializeField] Animator alertAnimator;
    [SerializeField] float alertDuration;

    bool countdownStarted = false;
    float cooldownPassed;
    float alertTimePassed;
    bool alertActive = false;

    PlayerAttack playerAttack;

    // Start is called before the first frame update
    void Start()
    {
        playerAttack = FindObjectOfType<PlayerAttack>();
        soulCost.text = playerAttack.SummonCost.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerAttack.SummonAvailable)
        {
            fade.SetActive(false);
            countdownStarted = false;
        }
        else
        {
            fade.SetActive(true);
            cooldownPassed -= Time.deltaTime;
            if (!countdownStarted)
            {
                countdownStarted = true;
                cooldownPassed = playerAttack.SummonCooldown;
            }
            cooldownText.text = (cooldownPassed < 0) ? "0" : Mathf.CeilToInt(cooldownPassed).ToString();
        }

        if (alertActive)
        {
            alertTimePassed -= Time.deltaTime;
            if (alertTimePassed < 0)
            {
                alertActive = false;
                alertAnimator.SetBool("alertActive", false);
            }
        }
    }

    public void ShowAlert()
    {
        alertTimePassed = alertDuration;
        alertActive = true;
        alertAnimator.SetBool("alertActive", true);
    }
}
