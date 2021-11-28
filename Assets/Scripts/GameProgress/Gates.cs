using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gates : MonoBehaviour
{
    [SerializeField] int initialDurability;
    [SerializeField] GameObject passThroughGateIndicator;

    int durability;

    public int Durability { get => durability; }

    public delegate void GateStatusUpdateAction();
    public event GateStatusUpdateAction GateInvaded;

    // Start is called before the first frame update
    void Start()
    {
        durability = initialDurability;
    }

    void OnTriggerEnter2D(Collider2D objectThatEntered)
    {
        if (objectThatEntered.tag == "Enemy" && !objectThatEntered.isTrigger)
        {
            StartCoroutine(EnemyPassedThroughGate(objectThatEntered.gameObject));
        }
    }

    IEnumerator EnemyPassedThroughGate(GameObject enemy)
    {
        GameObject indicatorParticle = Instantiate(passThroughGateIndicator, enemy.transform);
        indicatorParticle.transform.localPosition += Vector3.down * 0.01f;
        yield return new WaitForSeconds(0.5f);

        Destroy(enemy);
        durability--;
        if (durability <= 0)
        {
            GateInvaded();
        }
        yield return null;
    }
}
