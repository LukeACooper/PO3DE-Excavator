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
    public void FixedUpdate()
    {
        if(excavator.excavatorActive)
        {
            excavatorCamera.enabled = true;
            playerCamera.enabled = false;
            //excavatorCamera.GetComponent<AudioListener>().enabled = true;
            //playerCamera.GetComponent<AudioListener>().enabled = false;
        }
        else if(!excavator.excavatorActive)
        {
            excavatorCamera.enabled = false;
            playerCamera.enabled = true;
            //playerCamera.GetComponent<AudioListener>().enabled = true;
            //excavatorCamera.GetComponent<AudioListener>().enabled = false;
        }
      
    }
}
