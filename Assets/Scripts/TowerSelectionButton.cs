using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelectionButton : MonoBehaviour
{
    [SerializeField] TowerBuilder towerBuilder;

    public void buttonTowerCerberusPressed()
    {
        towerBuilder.BuildTowerCerberus();
    }
}
