using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneBehavior : MonoBehaviour
{

    public bool playerInLane;
    // Start is called before the first frame update
    void Start()
    {
        playerInLane = false;
    }

    void OnTriggerEnter2D(Collider2D otherCollider){
        if(otherCollider.name == "Player"){
            playerInLane = true;
        }
    }

    void OnTriggerExit2D(Collider2D otherCollider){
        if(otherCollider.name == "Player"){
            playerInLane = false;
        }
    }
}
