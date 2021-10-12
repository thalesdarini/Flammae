using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerBuilder : MonoBehaviour
{
    [Serializable]
    struct Tower
    {
        public GameObject towerPrefab;
        public int towerCost;
        public Text towerCostText;
    }

    [SerializeField] Tower towerCerberus;

    [SerializeField] GameObject pressToBuildUI;
    [SerializeField] GameObject chooseTowerUI;

    Renderer areaRenderer;
    GameObject playerInArea;

    bool cerberusFlashing;

    // Start is called before the first frame update
    void Start()
    {
        areaRenderer = GetComponent<Renderer>();
        playerInArea = null;
        cerberusFlashing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInArea != null)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                pressToBuildUI.SetActive(false);
                chooseTowerUI.SetActive(true);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D objectThatEntered)
    {
        if (objectThatEntered.tag == "Player")
        {
            areaRenderer.enabled = true;
            pressToBuildUI.SetActive(true);
            playerInArea = objectThatEntered.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D objectThatExited)
    {
        if (objectThatExited.tag == "Player")
        {
            areaRenderer.enabled = false;
            pressToBuildUI.SetActive(false);
            if(chooseTowerUI.activeSelf)
            {
                towerCerberus.towerCostText.color = new Color(214f/256f, 212f/256f, 0f/256f);
                chooseTowerUI.SetActive(false);
            }
            playerInArea = null;
        }
    }

    public void BuildTowerCerberus()
    {
        if(playerInArea.GetComponent<PlayerSoulCounter>().SpendSouls(towerCerberus.towerCost))
        {
            chooseTowerUI.SetActive(false);
            Instantiate(towerCerberus.towerPrefab, transform.position, transform.rotation);
            Destroy(transform.parent.gameObject);
        }
        else
        {
            if(!cerberusFlashing)
            {
                StartCoroutine(TextColorFlashRed(towerCerberus.towerCostText, "cerberus"));
            }
        }
    }

    IEnumerator TextColorFlashRed(Text text, String whichTower)
    {
        setFlashing("cerberus", true);
        for(int i=0; i<100; i++)
        {
            float currentR = (194 + (214 - 194) * (i / 100f))/256;
            float currentG = (24 + (212 - 24) * (i / 100f))/256;
            float currentB = (7 + (0 - 7) * (i / 100f))/256;
            text.color = new Color(currentR, currentG, currentB);
            yield return new WaitForSeconds(0.007f);
        }
        text.color = new Color(214f/256f, 212f/256f, 0f/256f);
        setFlashing("cerberus", false);
        yield return null;
    }

    void setFlashing(String whichTower, bool flashing)
    {
        if(whichTower == "cerberus")
        {
            cerberusFlashing = flashing;
        }
    }
}
