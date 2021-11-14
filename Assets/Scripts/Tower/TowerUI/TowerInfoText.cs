using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerInfoText : TowerFlashingText
{
    void Awake()
    {
        towerInteractorReference = transform.parent.parent.Find("AreaToInteract").GetComponent<TowerInteractor>();
        towerReference = towerInteractorReference.TowerCurrentlyOnTextDisplay;
        text = GetComponent<Text>();
        isFlashing = false;
        isUpdateable = true;
        baseColor = new Color(214f / 256f, 212f / 256f, 0f / 256f);

        DisplayText();
    }

    override public void DisplayText()
    {
        towerReference.TowerBehaviourScript.DefineBehaviourVariables();
        text.text = InfoTextForBehaviour();
    }

    string InfoTextForBehaviour()
    {
        if (towerReference.TowerBehaviourScript.Behaviour == "attack")
        {
            return "\nTiros por segundo:\n" + towerReference.TowerBehaviourScript.ShotsPerSec.ToString("F2") +
            "\nAlcance:\n" + towerReference.TowerBehaviourScript.ShootingRange.ToString("F2") + "\n" +
            "\nProj�til:\n" + "Dano        Velocidade\n" +
            towerReference.TowerBehaviourScript.ProjectileDamage.ToString("F2") + "              " +
            towerReference.TowerBehaviourScript.ProjectileSpeed.ToString("F2") + "    ";
        }
        else if (towerReference.TowerBehaviourScript.Behaviour == "buff")
        {
            return "\nBuff:\n+" + towerReference.TowerBehaviourScript.BuffAmount.ToString("F2") + " pontos de\n" +
            towerReference.TowerBehaviourScript.BuffedStat + "\n" +
            "\nRaio de efeito:\n" + towerReference.TowerBehaviourScript.BuffingRange.ToString("F2");
        }
        else if (towerReference.TowerBehaviourScript.Behaviour == "heal")
        {
            return "\nTempo at� cura completa (0 a 100%):\n" + towerReference.TowerBehaviourScript.TimeToHeal.ToString("F2") + " s";
        }
        else
        {
            return "Deu Erro!!!";
        }
    }
}
