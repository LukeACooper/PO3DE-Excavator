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
    public float moveSpeed;
    public bool excavatorActive = false;
    [SerializeField] bool isInTransition;
    public Transform seat;
    public Transform exitPoint;
    public float transitionSpeed = 0.2f;
    private Animator anim;
    public CharacterController excavatorController;
    public Transform cam;
    public float turnSmoothTime = -5f;
    public float turnSmoothVelocity;
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
    }
    private void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            isInTransition = true;
        }

        if (excavatorActive)
        {
            MyInput();
            Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput).normalized;

            if (direction.magnitude >= 0.1f)
            {
                anim.SetFloat(hash.speedFloat, animationSpeed, speedDampTime, Time.deltaTime);
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                excavatorController.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);
            }
            else
            {
                anim.SetFloat(hash.speedFloat, 0);
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
            isInTransition = false;
            excavatorActive = true;
            player.GetComponent<CapsuleCollider>().enabled = false;
            player.GetComponent<Rigidbody>().useGravity = false;
            player.GetComponent<CharacterController>().enabled = false;
        }
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
            excavatorActive = false;
            isInTransition = false;
            player.GetComponent<CapsuleCollider>().enabled = true;
            player.GetComponent<Rigidbody>().useGravity = true;
            player.GetComponent<CharacterController>().enabled = true;
        }
    }
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

}
