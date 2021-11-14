using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class TowerInteractor : MonoBehaviour
{
    [SerializeField] GameObject firePillar;

    protected Tower towerCurrentlyOnTextDisplay;
    protected GameObject pressUI;
    protected GameObject windowUI;
    protected Renderer areaRenderer;
    protected GameObject playerInArea;
    protected FirePillar thisFirePillar1;
    protected FirePillar thisFirePillar2;
    protected FirePillar thisFirePillar3;
    protected bool alreadyBuilding;

    public Tower TowerCurrentlyOnTextDisplay { get => towerCurrentlyOnTextDisplay; }

    public delegate void AskForTextUpdateAction();
    public event AskForTextUpdateAction UpdateText;
    public delegate void PurchaseFailAction(string towerName);
    public event PurchaseFailAction TowerPurchaseFail;

    // Start is called before the first frame update
    void Start()
    {
        pressUI = transform.parent.Find("PressE").gameObject;
        windowUI = transform.parent.Find("Window").gameObject;
        areaRenderer = GetComponent<Renderer>();
        playerInArea = null;
        alreadyBuilding = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInArea != null)
        {
            if (Input.GetKeyDown(KeyCode.E) && !alreadyBuilding)
            {
                pressUI.SetActive(false);
                windowUI.SetActive(true);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D objectThatEntered)
    {
        if (objectThatEntered.tag == "Player" && !alreadyBuilding)
        {
            areaRenderer.enabled = true;
            pressUI.SetActive(true);
            playerInArea = objectThatEntered.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D objectThatExited)
    {
        if (objectThatExited.tag == "Player" && !alreadyBuilding)
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

    protected void CreateBuilding(GameObject building, string InstantiateBuildingFuncName, float horizontalSize, float constructionTime)
    {
        alreadyBuilding = true;
        windowUI.SetActive(false);
        areaRenderer.enabled = false;

        thisFirePillar1 = Instantiate(firePillar, transform.position + (Vector3.down * transform.localScale.y * 0.4f), transform.rotation).GetComponent<FirePillar>();
        thisFirePillar1.transform.localScale = (Vector3.right + Vector3.up) * horizontalSize * 0.4f + Vector3.forward;
        thisFirePillar2 = Instantiate(firePillar, transform.position + (Vector3.right * horizontalSize * 0.3f) + (Vector3.down * transform.localScale.y * 0.2f), transform.rotation).GetComponent<FirePillar>();
        thisFirePillar2.transform.localScale = (Vector3.right + Vector3.up) * horizontalSize * 0.4f + Vector3.forward;
        thisFirePillar3 = Instantiate(firePillar, transform.position + (Vector3.left * horizontalSize * 0.3f) + (Vector3.down * transform.localScale.y * 0.2f), transform.rotation).GetComponent<FirePillar>();
        thisFirePillar3.transform.localScale = (Vector3.right + Vector3.up) * horizontalSize * 0.4f + Vector3.forward;

        Invoke(InstantiateBuildingFuncName, constructionTime);
    }

    protected void InstantiateBuilding(GameObject buildingPrefab)
    {
        Instantiate(buildingPrefab, transform.position, transform.rotation);
        thisFirePillar1.CommandStop();
        thisFirePillar2.CommandStop();
        thisFirePillar3.CommandStop();
        Destroy(transform.parent.gameObject);
    }

    protected void CallEventUpdateText()
    {
        UpdateText();
    }

    protected void CallEventTowerPurchaseFail(string towerName)
    {
        TowerPurchaseFail(towerName);
    }
}
