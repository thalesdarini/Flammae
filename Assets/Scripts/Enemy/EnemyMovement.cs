using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float defaultSpeed;

    int waypointIndex;
    List<GameObject> waypoints;
    LaneBehavior laneBehavior;
    BoxCollider2D waypointArea;
    Vector3 nextPosition;

    Rigidbody2D rb2d;
    Animator moveAnimation;
    SpriteRenderer sRenderer;
    EnemyAttack enemyAttack;

    List<Transform> currentTargets;
    float speed;

    public float DefaultSpeed { get => defaultSpeed; }
    public float Speed { set => speed = value; }
    public List<GameObject> Waypoints { set => waypoints = value; }
    public LaneBehavior LaneBehavior { get => laneBehavior; set => laneBehavior = value; }

    // Start is called before the first frame update
    void Start()
    {
        waypointIndex = 0;
        waypointArea = waypoints[waypointIndex].GetComponent<BoxCollider2D>();
        nextPosition = GetRandomPointInsideCollider(waypointArea);

        rb2d = GetComponent<Rigidbody2D>();
        moveAnimation = GetComponent<Animator>();
        sRenderer = GetComponent<SpriteRenderer>();
        enemyAttack = GetComponent<EnemyAttack>();

        currentTargets = new List<Transform>();
        speed = defaultSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        CheckNextMovement();
    }

    //Verifica se há um player a ser atacado.
    //Senão vai até o próximo waypoint.
    void CheckNextMovement()
    {
        if(enemyAttack.IsAttacking == false){
            if(currentTargets.Count != 0){
                foreach(Transform target in currentTargets){
                    if (enemyAttack.CanAttack(target)){
                        enemyAttack.Attack(target.gameObject);
                        return;
                    }
                    else if (laneBehavior.FoesInLane.Contains(target.gameObject)){
                        MoveTowardsFoe(target);
                        return;
                    }
                }
            }
            MoveTowardsWaypoint();
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

    void MoveTowardsFoe(Transform target)
    {
        currentTargets.RemoveAll(item => item == null);

        rb2d.velocity = (target.position - transform.position).normalized * speed;
        moveAnimation.SetBool("isMoving", true);

        //Flip sprite if player is on the left side.
        if (target.position.x - transform.position.x < 0) {
            sRenderer.flipX = true;
        }
        else sRenderer.flipX = false;
    }

    void MoveTowardsWaypoint()
    {
        Move(nextPosition);
		if ((nextPosition - transform.position).magnitude < 0.2f) {
			waypointIndex += 1;
            if(waypointIndex > waypoints.Count-1){
                EnemyReachesEnd();
            }
            else{
                getNextPosition();
            }
		}

        if (nextPosition.x - transform.position.x < 0) {
            sRenderer.flipX = true;
        }
        else sRenderer.flipX = false;
    }

    //Makes object moves towards a target.
    void Move(Vector3 target){
        rb2d.velocity = (target - transform.position).normalized * speed;
        moveAnimation.SetBool("isMoving", true);
    }

    void getNextPosition(){
        waypointArea = waypoints[waypointIndex].GetComponent<BoxCollider2D>();
        nextPosition = GetRandomPointInsideCollider(waypointArea);
    }

    void EnemyReachesEnd(){
        Destroy(gameObject);
    }

    //Upon entering the collider, enemies will follow player.
    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if(otherCollider.tag == "Player" || otherCollider.tag == "Infernais"){
            currentTargets.Add(otherCollider.transform);
        }
    }

    //When Player leaves area of agro, enemies go back to their root, searching the nearest waypoint.
    //It goes to the next waypoint if possible, to guarantee enemy does not go backwards.
    void OnTriggerExit2D(Collider2D otherCollider)
    {
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
