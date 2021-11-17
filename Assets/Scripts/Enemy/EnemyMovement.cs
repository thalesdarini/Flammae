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

    public List<Transform> currentTargets;

    LaneBehavior laneBehavior;
    SwordmanAttack swordmanAttack;

    // Start is called before the first frame update
    void Start()
    {
        waypointIndex = 0;
        waypointArea = waypoints[waypointIndex].GetComponent<BoxCollider2D>();
        nextPosition = GetRandomPointInsideCollider(waypointArea);

        rb2d = GetComponent<Rigidbody2D>();
        moveAnimation = GetComponent<Animator>();
        sRenderer = GetComponent<SpriteRenderer>();
        laneBehavior = FindObjectOfType<LaneBehavior>();
        swordmanAttack = FindObjectOfType<SwordmanAttack>();
        currentTargets = new List<Transform>();
    }

    void Update()
    {
        CheckNextMovement();
    }

    //Verifica se há um player a ser atacado.
    //Senão vai até o próximo waypoint.
    void CheckNextMovement(){
        
        if(swordmanAttack.IsAttacking == false){
            if(currentTargets.Count != 0){
                foreach(Transform target in currentTargets){
                    if(laneBehavior.FoesInLane.Contains(target.gameObject)){
                        MoveTowardsFoe(target);
                    }
                    else MoveTowardsWaypoint();
                }
            }
            else MoveTowardsWaypoint();
        }
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

    void MoveTowardsFoe(Transform target){

        currentTargets.RemoveAll(item => item == null);

        //Flip sprite if player is on the left side.
        if(this.transform.position.x - target.position.x > 0) {
            sRenderer.flipX = true;
        }

        else sRenderer.flipX = false;

        //Function on Swordman can attack
        if(swordmanAttack.CanAttack(target)){    //Ataca
            swordmanAttack.AttackPlayer(target.gameObject);
        }
        else{
            transform.position = Vector2.MoveTowards(this.transform.position,target.position, speed * Time.deltaTime);
            moveAnimation.SetBool("isMoving", true);

        }
    }

    void MoveTowardsWaypoint()
    {
        transform.position = Vector2.MoveTowards(this.transform.position, nextPosition, speed * Time.deltaTime);
		if ((nextPosition - transform.position).magnitude < 0.2f) {
			waypointIndex += 1;
            if(waypointIndex > waypoints.Count-1){
                EnemyReachesEnd();
            }
            else{
                getNextPosition();
            }
		}

        moveAnimation.SetBool("isMoving", true);
        
        if(this.transform.position.x - nextPosition.x > 0) {
            sRenderer.flipX = true;
        }
        else sRenderer.flipX = false;
    }

    void getNextPosition(){
        waypointArea = waypoints[waypointIndex].GetComponent<BoxCollider2D>();
        nextPosition = GetRandomPointInsideCollider(waypointArea);
    }

    void EnemyReachesEnd(){
        Destroy(gameObject);
    }

    //Upon entering the collider, enemies will follow player.
    void OnTriggerEnter2D(Collider2D otherCollider){

        if(otherCollider.tag == "Player" || otherCollider.tag == "Infernais"){
            currentTargets.Add(otherCollider.transform);
        }
    }

    //When Player leaves area of agro, enemies go back to their root, searching the nearest waypoint.
    //It goes to the next waypoint if possible, to guarantee enemy does not go backwards.
    void OnTriggerExit2D(Collider2D otherCollider){
        if(otherCollider.tag == "Player" || otherCollider.tag == "Infernais"){
            currentTargets.Remove(otherCollider.transform);

            //Go to the next nearest waypoint
            float minDist = Mathf.Infinity;

            for (int i = 0; i < waypoints.Count; i++)
            {
                float dist = Vector3.Distance(waypoints[i].transform.position, transform.position);
                if(dist < minDist){
                    waypointIndex = i;
                    minDist = dist;
                }
                i++;
            }
            if(waypointIndex <= waypoints.Count-1) waypointIndex++;
            getNextPosition();
        }
    }
}
