using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerFlashingText : MonoBehaviour
{
    protected Tower towerReference;
    protected TowerInteractor towerInteractorReference;
    protected Text text;
    protected bool isFlashing;

    void OnEnable()
    {
        text.color = new Color(214f / 256f, 212f / 256f, 0f / 256f);
        towerInteractorReference.TowerPurchaseFail += FlashText;
    }

    void OnDisable()
    {
        towerInteractorReference.TowerPurchaseFail -= FlashText;
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
            float currentR = (194 + (214 - 194) * (i / 100f)) / 256;
            float currentG = (24 + (212 - 24) * (i / 100f)) / 256;
            float currentB = (7 + (0 - 7) * (i / 100f)) / 256;
            text.color = new Color(currentR, currentG, currentB);
            yield return new WaitForSeconds(0.007f);
        }
        text.color = new Color(214f / 256f, 212f / 256f, 0f / 256f);
        isFlashing = false;
        yield return null;
    }
}
