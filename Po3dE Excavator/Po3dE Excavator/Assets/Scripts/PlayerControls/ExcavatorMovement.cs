using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcavatorMovement : MonoBehaviour
{
    public float speedDampTime = 3f;
    public float sensitivityX = 1.0f;
    public float animationSpeed = 1f;
    private Animator anim;
    private HashIDs hash;

    public Transform player;
    public bool excavatorActive;
    bool isInTransition;
    public Transform seat;
    public Vector3 sittingoffset;
    public Transform exitPoint;
    [Space]
    public float transitionSpeed = 0.2f;

    private void Update()
    {
        if(excavatorActive && isInTransition)
        {
            Exit();
        }
        else if(!excavatorActive && isInTransition)
        {
            Enter();
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            isInTransition = true;
        }
        if(excavatorActive)
        {

        }
    
    }
    private void Enter()
    {
        player.GetComponent<CapsuleCollider>().enabled = false;
        player.GetComponent<Rigidbody>().useGravity = false;

        player.position = Vector3.Lerp(player.position, seat.position + sittingoffset, transitionSpeed);
        player.rotation = Quaternion.Slerp(player.rotation, seat.rotation, transitionSpeed);

        if(player.position == seat.position + sittingoffset)
        {
            isInTransition = false;
            excavatorActive = true;
            
        }
    }
    private void Exit()
    {
        player.GetComponent<CapsuleCollider>().enabled = true;
        player.GetComponent<Rigidbody>().useGravity = true;
        player.position = Vector3.Lerp(player.position, exitPoint.position, transitionSpeed);
        player.rotation = Quaternion.Slerp(player.rotation, exitPoint.rotation, transitionSpeed);
        if(player.position == exitPoint.position)
        {
            excavatorActive = false;
            isInTransition = false;  
        }
    }
}
