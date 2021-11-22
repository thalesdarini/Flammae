using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] Animator cameraAnimator;

    // Update is called once per frame
    void Update()
    {
        float xAxis = 0;
        if (Input.GetKey(KeyCode.L))
            xAxis = 1;
        else if (Input.GetKey(KeyCode.J))
            xAxis = -1;

        float yAxis = 0;
        if (Input.GetKey(KeyCode.I))
            yAxis = 1;
        else if (Input.GetKey(KeyCode.K))
            yAxis = -1;

        transform.position += new Vector3(xAxis, yAxis, 0).normalized * movementSpeed * Time.deltaTime;


        if (Input.GetKeyDown(KeyCode.Z))
        {
            cameraAnimator.SetTrigger("zoom");
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            cameraAnimator.SetTrigger("unzoom");
        }
    }
}
