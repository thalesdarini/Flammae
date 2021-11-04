using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class TowerInteractor : MonoBehaviour
{
    protected GameObject pressUI;
    protected GameObject windowUI;
    protected Renderer areaRenderer;
    protected GameObject playerInArea;

    public delegate void PurchaseFailAction(string towerName);
    public event PurchaseFailAction TowerPurchaseFail;

    // Start is called before the first frame update
    void Start()
    {
        pressUI = transform.parent.Find("PressE").gameObject;
        windowUI = transform.parent.Find("Window").gameObject;
        areaRenderer = GetComponent<Renderer>();
        playerInArea = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInArea != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                pressUI.SetActive(false);
                windowUI.SetActive(true);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D objectThatEntered)
    {
        if (objectThatEntered.tag == "Player")
        {
            areaRenderer.enabled = true;
            pressUI.SetActive(true);
            playerInArea = objectThatEntered.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D objectThatExited)
    {
        if (objectThatExited.tag == "Player")
        {
            areaRenderer.enabled = false;
            pressUI.SetActive(false);
            if (windowUI.activeSelf)
            {
                windowUI.SetActive(false);
            }
            playerInArea = null;
        }
    }

    protected void callEventTowerPurchaseFail(string towerName)
    {
        TowerPurchaseFail(towerName);
    }
}
