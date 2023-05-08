using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speedDampTime = 0.1f;
    public float sensitivityX = 1.0f;
    public float animationSpeed = 1.5f;
    
    private Animator anim;
    private HashIDs hash;
    public ExcavatorMovement excavator;

    private void Awake()
    {
        if(!gameObject.CompareTag("Excavator"))
        {
            anim = GetComponent<Animator>();
            hash = GameObject.FindGameObjectWithTag("GameController").GetComponent<HashIDs>();
            anim.SetLayerWeight(1, 1f);
        }
        
    }

    private void FixedUpdate()
    {
        float v = Input.GetAxis("Vertical");
        bool sneak = Input.GetButton("Sneak");
        float turn = Input.GetAxis("Turn");
        Rotating(turn);
        MovementManagement(v, sneak);   
    }

    void Rotating(float mouseXInput)
    {
        Rigidbody ourBody = this.GetComponent<Rigidbody>();

        if(mouseXInput != 0)
        {
            Quaternion deltaRotation = Quaternion.Euler(0f, mouseXInput * sensitivityX, 0f);
            ourBody.MoveRotation(ourBody.rotation * deltaRotation); 
        }
    }
    void MovementManagement(float vertical, bool sneaking)
    {
        if(!gameObject.CompareTag("Excavator"))
        {
            anim.SetBool(hash.sneakingBool, sneaking);
            if (vertical > 0)
            {
                anim.SetFloat(hash.speedFloat, animationSpeed, speedDampTime, Time.fixedDeltaTime);
            }
            else
            {
                anim.SetFloat(hash.speedFloat, 0);
            }
        }
        
    }
}
