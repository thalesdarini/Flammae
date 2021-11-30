using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera zoomedIn;
    [SerializeField] CinemachineVirtualCamera zoomedOut;

    // Start is called before the first frame update
    void Start()
    {
        zoomedIn.Priority = 0;
        zoomedOut.Priority = -1;
    }

    // Update is called once per frame
    void Update()
    {
        float scroll = Input.mouseScrollDelta.y;
        if (scroll > 0) // up -> zoom in
        {
            zoomedOut.Priority = -1;
        }
        else if (scroll < 0) // down -> zoom out
        {
            zoomedOut.Priority = 1;
        }
    }
}
