using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject sword;
    [SerializeField] ContactFilter2D cf2d;
    PlayerMovement playerMovement;
    PolygonCollider2D swordCollider;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        swordCollider = sword.GetComponent<PolygonCollider2D>();
    }


    // Update is called once per frame
    void Update()
    {
        Vector2 facingDirection = playerMovement.GetFacingDirection();
        float angle = Vector2.SignedAngle(Vector2.down, facingDirection);
        sword.transform.localRotation = Quaternion.Euler(0, 0, angle);

        if (Input.GetButtonDown("Fire1"))
        {
            Collider2D[] results = new Collider2D[5];
            swordCollider.OverlapCollider(cf2d, results);
            foreach (Collider2D cd in results)
            {
                Debug.Log(cd);
            }
        }
    }
}
