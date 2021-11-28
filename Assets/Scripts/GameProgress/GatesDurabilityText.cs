using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GatesDurabilityText : MonoBehaviour
{
    Gates gatesReference;
    Text durabilityText;

    // Start is called before the first frame update
    void Start()
    {
        gatesReference = transform.parent.parent.GetComponent<Gates>();
        durabilityText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        durabilityText.text = gatesReference.Durability.ToString();
    }
}
