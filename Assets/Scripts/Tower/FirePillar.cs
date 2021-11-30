using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePillar : MonoBehaviour
{
    Animator animatorReference;
    bool stopping;

    // Start is called before the first frame update
    void Start()
    {
        animatorReference = transform.Find("Animation").GetComponent<Animator>();
        stopping = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (stopping && animatorReference.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            Destroy(gameObject);
        }
    }

    public void CommandStop()
    {
        animatorReference.SetTrigger("Finished");
        stopping = true;
    }
}
