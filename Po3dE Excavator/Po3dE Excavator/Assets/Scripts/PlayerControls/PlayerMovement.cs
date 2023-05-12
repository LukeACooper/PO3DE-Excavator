using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float turnSmoothTime = -100f;
    public float turnSmoothVelocity;
    public float animationSpeed = 1.5f;
    float horizontalInput;
    float verticalInput;
    Rigidbody rb;
    private Animator anim;
    private HashIDs hash;
    public ExcavatorMovement excavator;
    public CharacterController controller;
    public Transform cam;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag("GameController").GetComponent<HashIDs>();
        anim.SetLayerWeight(1, 1f);
        
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
   
    private void Update()
    {
        anim.SetFloat(hash.speedFloat, 0);
        if (!excavator.excavatorActive)
        {
            MyInput();
        }
        else if(excavator.excavatorActive)
        {
            rb.constraints = RigidbodyConstraints.FreezePosition;
        }
        Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);
            
        }
    }
    private void FixedUpdate()
    {
    }
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        
    }
 

}
