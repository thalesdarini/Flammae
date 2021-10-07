using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlTest : MonoBehaviour
{
    [SerializeField] float speed;
    Rigidbody2D thisRigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        thisRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        thisRigidbody2D.velocity = new Vector2(Input.GetAxisRaw("Horizontal")*speed, Input.GetAxisRaw("Vertical")*speed);
    }
}
