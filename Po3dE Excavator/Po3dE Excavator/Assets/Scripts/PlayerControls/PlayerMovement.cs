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
        anim = GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag("GameController").GetComponent<HashIDs>();
        anim.SetLayerWeight(1, 1f);
    }

    private void FixedUpdate()
    {
        if(excavator.excavatorActive == false)
        {
            float v = Input.GetAxis("Vertical");
            bool sneak = Input.GetButton("Sneak");
            float turn = Input.GetAxis("Turn");
            Rotating(turn);
            MovementManagement(v, sneak);
        }
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
        if(excavator.excavatorActive == false)
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

    void AudioManagement (bool shout)
    {
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
        {
            if(!GetComponent<AudioSource>().isPlaying)
            {
                GetComponent<AudioSource>().pitch = 0.27f;
                GetComponent<AudioSource>().Play();
            }
            else
            {
                GetComponent<AudioSource>().Stop();
            }
        }

    }
}
