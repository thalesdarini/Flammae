using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerUpgrader : MonoBehaviour
{
    [Serializable]
    struct Tower
    {
        public GameObject towerPrefab;
        public int upgradeCost;
        public Text upgradeCostText;
    }

    [SerializeField] Tower upgrade;

    [SerializeField] GameObject pressToUpgradeUI;
    [SerializeField] GameObject upgradeUI;
    [SerializeField] GameObject buildTowerPrefab;
    [SerializeField] int deconstructionBonus;

    Renderer areaRenderer;
    GameObject playerInArea;

    bool flashing;

    // Start is called before the first frame update
    void Start()
    {
        areaRenderer = GetComponent<Renderer>();
        playerInArea = null;
        flashing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInArea != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                pressToUpgradeUI.SetActive(false);
                upgradeUI.SetActive(true);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D objectThatEntered)
    {
        if (objectThatEntered.tag == "Player")
        {
            areaRenderer.enabled = true;
            pressToUpgradeUI.SetActive(true);
            playerInArea = objectThatEntered.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D objectThatExited)
    {
        if (objectThatExited.tag == "Player")
        {
            areaRenderer.enabled = false;
            pressToUpgradeUI.SetActive(false);
            if (upgradeUI.activeSelf)
            {
                upgrade.upgradeCostText.color = new Color(214f / 256f, 212f / 256f, 0f / 256f);
                upgradeUI.SetActive(false);
            }
            playerInArea = null;
        }
    }

    public void UpgradeTower()
    {
        if (playerInArea.GetComponent<PlayerSoulCounter>().SpendSouls(upgrade.upgradeCost))
        {
            upgradeUI.SetActive(false);
            Instantiate(upgrade.towerPrefab, transform.position, transform.rotation);
            Destroy(transform.parent.gameObject);
        }
        else
        {
            if (!flashing)
            {
                StartCoroutine(TextColorFlashRed(upgrade.upgradeCostText));
            }
        }
    }

    public void DeconstructTower()
    {
        upgradeUI.SetActive(false);
        playerInArea.GetComponent<PlayerSoulCounter>().CollectSouls(deconstructionBonus);
        Instantiate(buildTowerPrefab, transform.position, transform.rotation);
        Destroy(transform.parent.gameObject);
    }

    IEnumerator TextColorFlashRed(Text text)
    {
        flashing = true;
        for (int i = 0; i < 100; i++)
        {
            float currentR = (194 + (214 - 194) * (i / 100f)) / 256;
            float currentG = (24 + (212 - 24) * (i / 100f)) / 256;
            float currentB = (7 + (0 - 7) * (i / 100f)) / 256;
            text.color = new Color(currentR, currentG, currentB);
            yield return new WaitForSeconds(0.007f);
        }
        text.color = new Color(214f / 256f, 212f / 256f, 0f / 256f);
        flashing = false;
        yield return null;
    }
}
