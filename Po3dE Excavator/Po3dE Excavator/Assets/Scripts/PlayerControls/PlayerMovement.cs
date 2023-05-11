using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float turnSmoothTime = 0.1f;
    public float turnSmoothVelocity;
    public float animationSpeed = 1.5f;
    float horizontalInput;
    float verticalInput;
    GameObject player;
    Rigidbody rb;
    private Animator anim;
    private HashIDs hash;
    public ExcavatorMovement excavator;
    public CharacterController controller;
    public Transform cam;
    public Camera playerCamera;
    public Camera excavatorCamera;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag("GameController").GetComponent<HashIDs>();
        anim.SetLayerWeight(1, 1f);
        playerCamera.enabled = true;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        
    }

    private void Update()
    {
        MyInput();

        Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);
            anim.SetFloat(hash.speedFloat, 0);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Excavator")
        {
            playerCamera.enabled = false;
            excavatorCamera.enabled = true;
            player.GetComponent<Rigidbody>().useGravity = false;
            player.GetComponent<CharacterController>().enabled = false;
        }
        else
        {
            player.GetComponent<Rigidbody>().useGravity = true;
            player.GetComponent<CharacterController>().enabled = true;
            excavatorCamera.enabled = false;
            playerCamera.enabled = true;
        }
    }

    private void FixedUpdate()
    {

    }

    private void MyInput()
    {
        if(!excavator.excavatorActive)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");
        }
        
    }

}
