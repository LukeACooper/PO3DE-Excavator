using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcavatorMovement : MonoBehaviour
{
    public float animationSpeed = 1f;
    public float speedDampTime = 0.01f;
    public Transform player;
    public Transform excavator;
    public Transform rotator;
    public float moveSpeed;
    public bool excavatorActive = false;
    bool isInTransition;
    public Transform seat;
    public Transform exitPoint;
    public float transitionSpeed = 0.2f;
    public CharacterController excavatorController;
    public Transform cam;
    public float turnSmoothTime = -5f;
    public float turnSmoothVelocity;
    private Animator anim;
    private HashIDs hash;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag("GameController").GetComponent<HashIDs>();
    }

    private void FixedUpdate()
    {
        if(!excavatorActive)
        {
            anim.SetBool(hash.drivingLeftBool, false);
            anim.SetBool(hash.drivingRightBool, false);
        }
        if (!excavatorActive && isInTransition)
        {
            Enter();
        }
        else if (excavatorActive && isInTransition)
        {
            Exit();
        }
    }
    private void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            isInTransition = true;
        }
        
        if (excavatorActive)
        {
           if(Input.GetButton("WheelOn"))
            {
                Debug.Log("spin");
                anim.SetBool(hash.spinningBool, true);
            }
            if (Input.GetButton("WheelOff"))
            {
                Debug.Log("no more spin");
                anim.SetBool(hash.spinningBool, false);
            }
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");       
            Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput).normalized;

            if (direction.magnitude >= 0.2f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                excavatorController.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);   
                anim.SetBool(hash.drivingLeftBool, true);
                anim.SetBool(hash.drivingRightBool, true);
                
            }
            else
            {
                anim.SetBool(hash.drivingLeftBool, false);
                anim.SetBool(hash.drivingRightBool, false);
            } 
        }
    }

    private void Enter()
    {
        //Makes the player a child of the excavator whilst it is activated and places it in the seat position, disabling all physics on the player until 'Exit' is performed.

        player.position = Vector3.Lerp(player.position, seat.position, transitionSpeed);
        player.rotation = Quaternion.Slerp(player.rotation, seat.rotation, transitionSpeed);

        if(player.position.x == seat.position.x)
        {
            player.transform.parent = seat.transform;
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            player.GetComponent<CapsuleCollider>().enabled = false;
            player.GetComponent<Rigidbody>().useGravity = false;
            player.GetComponent<CharacterController>().enabled = false;
        }
        isInTransition = false;
            excavatorActive = true;
            
    }
    private void Exit()
    {
        //Restores physics of the player and places them at the bottom of the ladder, also un-parenting it and disabling the excavator.
        player.transform.parent = null;
        player.position = Vector3.Lerp(player.position, exitPoint.position, transitionSpeed);
        player.rotation = Quaternion.Slerp(player.rotation, exitPoint.rotation, transitionSpeed);

        if(player.position == exitPoint.position)
        {
            player.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePosition;
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            player.GetComponent<CapsuleCollider>().enabled = true;
            player.GetComponent<Rigidbody>().useGravity = true;
            player.GetComponent<CharacterController>().enabled = true;
            excavatorActive = false;
            isInTransition = false;
           
        }
    }
    
}
