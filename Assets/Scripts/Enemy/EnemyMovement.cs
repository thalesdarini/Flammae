using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public List<GameObject> Waypoints { set => waypoints = value; }

    [SerializeField] public float speed;

    Rigidbody2D rb2d;
    Animator moveAnimation;
    SpriteRenderer sRenderer;

    List<GameObject> waypoints;
    int waypointIndex;

    // Start is called before the first frame update
    void Start()
    {
        waypointIndex = 0;
        rb2d = GetComponent<Rigidbody2D>();
        moveAnimation = GetComponent<Animator>();
        sRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        MoveTowardsWaypoint();
    }

    void MoveTowardsWaypoint()
    {
        rb2d.velocity = (waypoints[waypointIndex].transform.position - transform.position).normalized * speed;
		if ((waypoints[waypointIndex].transform.position - transform.position).magnitude < 0.02f && waypointIndex < waypoints.Count-1) {
			waypointIndex += 1;
		}

        moveAnimation.SetBool("isMoving", true);
        
        if(rb2d.velocity.x < 0) {
            sRenderer.flipX = true;
        }
        else sRenderer.flipX = false;
        
    }
}
