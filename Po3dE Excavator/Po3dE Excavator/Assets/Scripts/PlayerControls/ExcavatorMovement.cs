using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcavatorMovement : MonoBehaviour
{
    public float speedDampTime = 3f;
    public float sensitivityX = 1.0f;
    public float animationSpeed = 1f;
    public Transform player;
    public bool excavatorActive = false;
    bool isInTransition;
    public Transform seat;
    public Vector3 sittingoffset;
    public Transform exitPoint;
    [Space]
    public float transitionSpeed = 0.2f;
    private Animator anim;
    private HashIDs hash;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag("GameController").GetComponent<HashIDs>();
        anim.SetLayerWeight(1, 1f);
    }

    private void FixedUpdate()
    {

        if (!excavatorActive && isInTransition)
        {
            Enter();
        }
        else if (excavatorActive && isInTransition)
        {
            Exit();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            isInTransition = true;
        }

        if (excavatorActive)
        {
            float verticalInput = Input.GetAxisRaw("Vertical");
            float turn = Input.GetAxis("Rotate");
            bool spin = Input.GetButton("ExcavatorSpin");
            Rotating(turn);
            MovementManagement(verticalInput, spin);

        }

    }


    void Rotating(float mouseXInput)
    {
        Rigidbody ourBody = this.GetComponent<Rigidbody>();

        if (mouseXInput != 0)
        {
            Quaternion deltaRotation = Quaternion.Euler(0f, mouseXInput * sensitivityX, 0f);
            ourBody.MoveRotation(ourBody.rotation * deltaRotation);
        }
    }
    void MovementManagement(float vertical, bool spin)
    {
        if (vertical > 0)
        {
            anim.SetFloat(hash.speedFloat, animationSpeed, speedDampTime, Time.fixedDeltaTime);
        }
        else
        {
            anim.SetFloat(hash.speedFloat, 0);
        }

    }
    private void Enter()
    {
        
        player.GetComponent<CharacterController>().enabled = false;
        player.position = Vector3.Lerp(player.position, seat.position + sittingoffset, transitionSpeed);
        player.rotation = Quaternion.Slerp(player.rotation, seat.rotation, transitionSpeed);
        excavatorActive = true;
        if (player.position == seat.position + sittingoffset)
        {
            isInTransition = false;
            
        }
    }
    private void Exit()
    {
        player.position = Vector3.Lerp(player.position, exitPoint.position, transitionSpeed);
        player.rotation = Quaternion.Slerp(player.rotation, exitPoint.rotation, transitionSpeed);
        excavatorActive = false;
        if (player.position == exitPoint.position)
        { 
            isInTransition = false;
            player.GetComponent<CapsuleCollider>().enabled = true;
            player.GetComponent<Rigidbody>().useGravity = true;
        }
    }

}
