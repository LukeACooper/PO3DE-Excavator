using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcavatorMovement : MonoBehaviour
{
    public float animationSpeed = 1f;
    public float speedDampTime = 0.01f;
    public Transform player;
    public Transform excavator;
    float verticalInput;
    float horizontalInput;
    float rotationInput;
    public float moveSpeed;
    public bool excavatorActive = false;
    bool isInTransition;
    public Transform seat;
    public Transform exitPoint;
    public bool spinning = false;
    public float transitionSpeed = 0.2f;
    public CharacterController excavatorController;
    public Transform cam;
    public float turnSmoothTime = -5f;
    public float turnSmoothVelocity;

    private void Awake()
    {
        
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
                spinning = true;
                if(Input.GetButton("WheelOff"))
                {
                    spinning = false;
                }
            }
           while(spinning)
            {

            }

            MyInput();
            Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput).normalized;
            Vector3 rotation = new Vector3(0f, rotationInput, 0f);

            if (rotationInput >= 0.1f)
            {
                
            }
            else if(rotationInput <= -0.1f)
            { 

            }

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle -0.25f, 0f);
                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                excavatorController.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);   
                
            }
            else
            {
                
            } 
        }
    }

    private void Enter()
    {

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
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        rotationInput = Input.GetAxisRaw("ExRotate");

    }
}
