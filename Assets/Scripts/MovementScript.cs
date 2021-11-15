using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class MovementScript : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float defaultMovementSpeed;

    protected float movementSpeed;
    bool isBuffed;

    public bool IsBuffed { get => isBuffed; }

    // Start is called before the first frame update
    virtual protected void Start()
    {
        movementSpeed = defaultMovementSpeed;
        isBuffed = false;
    }

    public void SpeedModify(float buff)
    {
        movementSpeed += buff;

        if (movementSpeed == defaultMovementSpeed)
        {
            isBuffed = false;
        }
        else
        {
            isBuffed = true;
        }
    }
}
