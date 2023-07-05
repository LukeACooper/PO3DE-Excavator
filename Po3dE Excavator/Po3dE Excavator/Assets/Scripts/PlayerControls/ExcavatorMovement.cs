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
    public Transform rotator;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag("GameController").GetComponent<HashIDs>();
        
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
<<<<<<< Updated upstream
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");       
            Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput).normalized;
=======
            if(Input.GetButtonDown("RotateRight"))
            {
                Debug.Log("right turn");
                rotator.transform.Rotate(0, 0.5f, 0f * Time.deltaTime);
            }
            if (Input.GetButtonDown("RotateLeft"))
            {
                Debug.Log("left turn");
                rotator.transform.Rotate(0, -0.5f, 0f * Time.deltaTime);
            }

            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");
            //float rotationInput = Input.GetAxisRaw("Rotate");

            Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput).normalized;
           // Vector3 rotation = new Vector3(0f, rotationInput, 0f);

            
>>>>>>> Stashed changes

            if (direction.magnitude >= 0.2f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                excavatorController.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);
<<<<<<< Updated upstream
                anim.SetFloat(hash.leftMoveFloat, animationSpeed, speedDampTime, Time.deltaTime);
                anim.SetFloat(hash.rightMoveFloat, animationSpeed, speedDampTime, Time.deltaTime);
            }
            else
            {
                anim.SetFloat(hash.leftMoveFloat, 0f);
                anim.SetFloat(hash.rightMoveFloat, 0f);
=======
                anim.SetBool(hash.drivingBool, true);

            }
            else
            {
                anim.SetBool(hash.drivingBool, false);
>>>>>>> Stashed changes
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
