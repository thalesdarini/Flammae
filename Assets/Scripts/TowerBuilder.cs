using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuilder : MonoBehaviour
{
    [SerializeField] GameObject towerCerberus;
    [SerializeField] GameObject indicatorToBuild;
    [SerializeField] GameObject pressToBuildUI;
    [SerializeField] GameObject chooseTowerUI;

    Renderer areaRenderer;
    bool playerInArea;

    // Start is called before the first frame update
    void Start()
    {
        areaRenderer = GetComponent<Renderer>();
        playerInArea = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInArea)
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
            playerInArea = true;
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
                chooseTowerUI.SetActive(false);
            }
            playerInArea = false;
        }
    }

    public void BuildTowerCerberus()
    {
        chooseTowerUI.SetActive(false);
        Instantiate(towerCerberus, indicatorToBuild.transform.position + Vector3.up * (towerCerberus.transform.localScale.y - indicatorToBuild.transform.localScale.y) / 2, indicatorToBuild.transform.rotation);
        Destroy(transform.parent.gameObject);
    }
}
