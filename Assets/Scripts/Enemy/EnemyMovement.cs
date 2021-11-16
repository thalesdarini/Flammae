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

    BoxCollider2D waypointArea;
    Vector3 nextPosition;

    // Start is called before the first frame update
    void Start()
    {
        waypointIndex = 0;
        waypointArea = waypoints[waypointIndex].GetComponent<BoxCollider2D>();
        nextPosition = GetRandomPointInsideCollider(waypointArea);
        rb2d = GetComponent<Rigidbody2D>();
        moveAnimation = GetComponent<Animator>();
        sRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        MoveTowardsWaypoint();
        print(waypointIndex);
        print(nextPosition);
    }

    //Select random transform inside waypoint bounds.
    public Vector3 GetRandomPointInsideCollider( BoxCollider2D boxCollider )
    {
        Vector3 extents = boxCollider.size / 2f;
        Vector3 point = new Vector3(
            Random.Range( -extents.x, extents.x ),
            Random.Range( -extents.y, extents.y ),
            Random.Range( -extents.z, extents.z )
        );
    
        return boxCollider.transform.TransformPoint( point );
    }

    void MoveTowardsWaypoint()
    {
        rb2d.velocity = (nextPosition - transform.position).normalized * speed;
		if ((nextPosition - transform.position).magnitude < 0.2f) {
			waypointIndex += 1;
            if(waypointIndex > waypoints.Count-1){
                EnemyReachesEnd();
            }
            else{
                waypointArea = waypoints[waypointIndex].GetComponent<BoxCollider2D>();
                nextPosition = GetRandomPointInsideCollider(waypointArea);
            }
		}

        moveAnimation.SetBool("isMoving", true);
        
        if(rb2d.velocity.x < 0) {
            sRenderer.flipX = true;
        }
        else sRenderer.flipX = false;
    }

    void EnemyReachesEnd(){
        Destroy(gameObject);
    }
}
