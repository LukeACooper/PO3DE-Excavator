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
    }

    private void Start()
    {
     
    }
    public void Update()
    {
        if(excavator.excavatorActive)
        {

            playerCamera.enabled = false;
            excavatorCamera.enabled = true;

        }
        else if(excavator.excavatorActive == false)
        {
            excavatorCamera.enabled = false;
            playerCamera.enabled = true;

        }
      
    }
}
