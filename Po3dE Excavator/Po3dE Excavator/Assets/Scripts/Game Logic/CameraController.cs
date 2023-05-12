using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public ExcavatorMovement excavator;
    public Camera playerCamera;
    public Camera excavatorCamera;


    public void Awake()
    {
        playerCamera.enabled = true;
        excavatorCamera.enabled = false;
    }

    
    public void Update()
    {
        if(excavator.excavatorActive == true)
        {
            excavatorCamera.enabled = true;
            playerCamera.enabled = false;

        }

        else if(excavator.excavatorActive == false)
        {
            excavatorCamera.enabled = false;
            playerCamera.enabled = true;
        }
      
    }
}
