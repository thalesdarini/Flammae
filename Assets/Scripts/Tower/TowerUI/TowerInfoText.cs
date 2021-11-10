using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerInfoText : TowerFlashingText
{
    [SerializeField] Tower towerReference;

    void Awake()
    {
        towerRef = towerReference;
        towerInteractorReference = transform.parent.parent.Find("AreaToInteract").GetComponent<TowerInteractor>();
        text = GetComponent<Text>();
        isFlashing = false;
        baseColor = new Color(214f / 256f, 212f / 256f, 0f / 256f);

        DisplayText();
    }

    override public void DisplayText()
    {
        towerRef.TowerBehaviourScript.DefineBehaviourVariables();
        text.text = InfoTextForBehaviour();
    }

    string InfoTextForBehaviour()
    {
        if (towerRef.TowerBehaviourScript.Behaviour == "attack")
        {
            return "\nTiros por segundo:\n" + towerRef.TowerBehaviourScript.ShotsPerSec.ToString("F2") +
            "\nAlcance:\n" + towerRef.TowerBehaviourScript.ShootingRange.ToString("F2") + "\n" +
            "\nProjétil:\n" + "Dano        Velocidade\n" +
            towerRef.TowerBehaviourScript.ProjectileDamage.ToString("F2") + "              " +
            towerRef.TowerBehaviourScript.ProjectileSpeed.ToString("F2") + "    ";
        }
        else if (towerRef.TowerBehaviourScript.Behaviour == "buff")
        {
            return "Buff:\n+" + towerRef.TowerBehaviourScript.BuffAmount.ToString("F2") +
            " pontos de " + towerRef.TowerBehaviourScript.BuffedStat +
            "\nRaio de efeito:\n" + towerRef.TowerBehaviourScript.BuffingRange.ToString("F2");
        }
        else if (towerRef.TowerBehaviourScript.Behaviour == "heal")
        {
            return "Tempo até cura completa (0 a 100%):\n" + towerRef.TowerBehaviourScript.TimeToHeal.ToString("F2") + " s";
        }
        else
        {
            return "Deu Erro!!!";
        }
    }
}
