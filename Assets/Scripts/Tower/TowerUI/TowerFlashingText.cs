using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

abstract public class TowerFlashingText : MonoBehaviour
{
    protected TowerInteractor towerInteractorReference;
    protected Tower towerReference;
    protected Text text;
    protected bool isFlashing;
    protected bool isUpdateable;
    protected Color baseColor;

    Color redForFlash = new Color(194f / 256f, 24f / 256f, 7f / 256f);
    float flashDuration = 0.7f;

    abstract public void DisplayText();

    void OnEnable()
    {
        text.color = baseColor;
        towerInteractorReference.TowerPurchaseFail += FlashText;
        towerInteractorReference.UpdateText += UpdateTextAccordingToTower;
    }

    void OnDisable()
    {
        towerInteractorReference.TowerPurchaseFail -= FlashText;
        towerInteractorReference.UpdateText -= UpdateTextAccordingToTower;
    }

    void UpdateTextAccordingToTower()
    {
        if (isUpdateable)
        {
            towerReference = towerInteractorReference.TowerCurrentlyOnTextDisplay;
            DisplayText();
        }
    }

    void FlashText(string towerName)
    {
        if (towerName == towerReference.TowerName && !isFlashing)
        {
            StartCoroutine(TextColorFlashRed());
        }
    }

    IEnumerator TextColorFlashRed()
    {
        isFlashing = true;

        for (int i = 0; i < 100; i++)
        {
            text.color = Color.Lerp(redForFlash, baseColor, i / 100f);
            yield return new WaitForSeconds(flashDuration / 100f);
        }
        text.color = baseColor;
        isFlashing = false;
        yield return null;
    }
}
