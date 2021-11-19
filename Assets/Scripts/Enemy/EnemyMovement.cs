using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public List<GameObject> Waypoints { set => waypoints = value; }
    //public List<Transform> CurrentTargets { get => currentTargets; }

    [SerializeField] public float speed;

    Rigidbody2D rb2d;
    Animator moveAnimation;
    SpriteRenderer sRenderer;

    public List<Transform> currentTargets;
    List<GameObject> waypoints;
    int waypointIndex;

    BoxCollider2D waypointArea;
    Vector3 nextPosition;

    LaneBehavior laneBehavior;
    SwordmanAttack swordmanAttack;
    ArcherAttack archerAttack;

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
        archerAttack = FindObjectOfType<ArcherAttack>();
        currentTargets = new List<Transform>();
    }

    void Update()
    {
        CheckNextMovement();
    }

    bool checkIfAttacking(){
        switch(this.name){
            case "EnemySwordman(Clone)":
                return swordmanAttack.IsAttacking;
            
            case "EnemyArcher(Clone)":
                return archerAttack.IsAttacking;
        }

        return true;
    }

    //Verifica se há um player a ser atacado.
    //Senão vai até o próximo waypoint.
    void CheckNextMovement(){
        
        //Lógica de movimentação
        if(!checkIfAttacking()){      //Se estiver atacando, não se move;
            if(currentTargets.Count != 0){  //Se não tiver alvos, move para o próximo waypoint
                foreach(Transform target in currentTargets){    //Checa se os alvos estão na lane, se não estiverem, move para o próximo waypoint.
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

        switch(this.name){
            case "EnemySwordman(Clone)":
                //Function on SwordmanAttack
                if(swordmanAttack.CanAttack(target)){    //Ataca
                    swordmanAttack.AttackFoe(target.gameObject);
                }
                else Move(target.position);
                break;
            
            case "EnemyArcher(Clone)":
                //Function on ArcherAttack
                if(archerAttack.CanAttack(target)){    //Ataca
                    archerAttack.AttackFoe(target.gameObject);
                }
                else Move(target.position);
                break;
        }
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
        
        if(this.transform.position.x - nextPosition.x > 0) {
            sRenderer.flipX = true;
        }
        else sRenderer.flipX = false;
    }

    //Makes object moves towards a target.
    void Move(Vector3 target){
        transform.position = Vector2.MoveTowards(this.transform.position, target, speed * Time.deltaTime);
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
