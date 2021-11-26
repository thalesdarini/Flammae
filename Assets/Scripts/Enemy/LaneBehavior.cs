using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneBehavior : MonoBehaviour
{
    public List<GameObject> FoesInLane { get => foesInLane; }
    List<GameObject> foesInLane;

    // Start is called before the first frame update
    void Start()
    {
        foesInLane = new List<GameObject>();
    }

    void OnTriggerEnter2D(Collider2D otherCollider){
        if(otherCollider.tag == "Player" || otherCollider.tag == "Infernais"){
            foesInLane.Add(otherCollider.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D otherCollider){
        if(otherCollider.tag == "Player" || otherCollider.tag == "Infernais"){
            foesInLane.Remove(otherCollider.gameObject);
        }
    }
}
