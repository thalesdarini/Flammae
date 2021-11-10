using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

abstract public class TowerFlashingText : MonoBehaviour
{
    protected Tower towerRef;
    protected TowerInteractor towerInteractorReference;
    protected Text text;
    protected bool isFlashing;

    protected Color baseColor;
    Color redForFlash = new Color(194f / 256f, 24f / 256f, 7f / 256f);

    abstract public void DisplayText();

    public Tower TowerRef { set => towerRef = value; }

    void OnEnable()
    {
        text.color = baseColor;
        towerInteractorReference.TowerPurchaseFail += FlashText;
    }

    void OnDisable()
    {
        towerInteractorReference.TowerPurchaseFail -= FlashText;
    }

    void FlashText(string towerName)
    {
        if (towerName == towerRef.TowerName && !isFlashing)
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
            yield return new WaitForSeconds(0.007f);
        }
        text.color = baseColor;
        isFlashing = false;
        yield return null;
    }
}
