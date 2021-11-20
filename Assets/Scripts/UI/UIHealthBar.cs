using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHealthBar : MonoBehaviour
{
    PlayerHealth playerReference;
    float yMaxScale;

    // Start is called before the first frame update
    void Start()
    {
        playerReference = CharacterList.playersAlive[0].GetComponent<PlayerHealth>();
        yMaxScale = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        float yScale = Mathf.Lerp(0f, yMaxScale, playerReference.HealthPercentual);
        transform.localScale = new Vector3(transform.localScale.x, yScale, transform.localScale.z);
    }
}
