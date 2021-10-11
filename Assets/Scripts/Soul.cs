using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{
    [SerializeField] float oscillationTime;
    [SerializeField] float oscillationHeight;

    float oscillationTimeBegin;
    float oscillationHeightBegin;

    // Start is called before the first frame update
    void Start()
    {
        oscillationTimeBegin = Time.time + Random.Range(-1, 1);
        oscillationHeightBegin = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        float heightFunction = oscillationHeightBegin + oscillationHeight * Mathf.Sin((2 * Mathf.PI / oscillationTime) * (Time.time - oscillationTimeBegin));
        transform.position = new Vector2(transform.position.x, heightFunction);
    }

    void OnTriggerEnter2D(Collider2D objectThatEntered)
    {
        if (objectThatEntered.tag == "Player")
        {
            objectThatEntered.GetComponent<PlayerControlTest>().CollectSouls(1);
            Destroy(gameObject);
        }
    }
}
