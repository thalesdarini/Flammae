using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] string towerName;
    [SerializeField] TowerBehaviour towerBehaviourScript;
    [SerializeField] GameObject towerPrefab;
    [SerializeField] int constructionCost;
    [SerializeField] GameObject towerBuilderReference;
    [SerializeField] int deconstructionBonus;

    public string TowerName { get => towerName; }
    public TowerBehaviour TowerBehaviourScript { get => towerBehaviourScript; }
    public GameObject TowerPrefab { get => towerPrefab; }
    public int ConstructionCost { get => constructionCost; }
    public GameObject TowerBuilderReference { get => towerBuilderReference; }
    public int DeconstructionBonus { get => deconstructionBonus; }
}
