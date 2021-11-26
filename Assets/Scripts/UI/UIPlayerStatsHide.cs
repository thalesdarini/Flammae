using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerStatsHide : MonoBehaviour
{
    bool isPopped;
    bool isRetracted;
    bool shouldBePopped;
    bool shouldBeRetracted;
    bool inAnimation;

    float poppedX = -29f;
    float retractedX = -214f;
    float popBurst = 15f;
    float retractBurst = 10f;

    // Start is called before the first frame update
    void Start()
    {
        isPopped = false;
        isRetracted = true;
        shouldBePopped = false;
        shouldBeRetracted = true;
        inAnimation = false;

        transform.localPosition = Vector3.right * retractedX;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRetracted && shouldBePopped && !inAnimation)
        {
            StartCoroutine(PopUpAnimation());
        }
        if (isPopped && shouldBeRetracted && !inAnimation)
        {
            StartCoroutine(RetractAnimation());
        }
    }

    IEnumerator PopUpAnimation()
    {
        inAnimation = true;

        for (int i = 0; i < 20; i++)
        {
            transform.localPosition = Vector3.right * Mathf.Lerp(retractedX, poppedX + popBurst, i / 20f);
            yield return new WaitForSeconds(0.4f / 20f);
        }
        for (int i = 0; i < 20; i++)
        {
            transform.localPosition = Vector3.right * Mathf.Lerp(poppedX + popBurst, poppedX, i / 20f);
            yield return new WaitForSeconds(0.2f / 20f);
        }

        isRetracted = false;
        isPopped = true;
        inAnimation = false;
        yield return null;
    }

    IEnumerator RetractAnimation()
    {
        inAnimation = true;

        for (int i = 0; i < 20; i++)
        {
            transform.localPosition = Vector3.right * Mathf.Lerp(poppedX, retractedX - retractBurst, i / 20f);
            yield return new WaitForSeconds(0.5f / 20f);
        }
        for (int i = 0; i < 20; i++)
        {
            transform.localPosition = Vector3.right * Mathf.Lerp(retractedX - retractBurst, retractedX, i / 20f);
            yield return new WaitForSeconds(0.2f / 20f);
        }

        isRetracted = true;
        isPopped = false;
        inAnimation = false;
        yield return null;
    }

    public void PopUp()
    {
        shouldBePopped = true;
        shouldBeRetracted = false;
    }

    public void Retract()
    {
        shouldBePopped = false;
        shouldBeRetracted = true;
    }
}
