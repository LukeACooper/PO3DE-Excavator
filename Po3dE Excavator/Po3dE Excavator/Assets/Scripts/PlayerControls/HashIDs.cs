using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HashIDs : MonoBehaviour
{
    public int dyingState;
    public int deadBool;
    public int walkState;
    public int shoutState;
    public int speedFloat;
    public int sneakingBool;
    public int shoutingBool;
    public int leftMoveState;
    public int rightMoveState;
    public int spinningBool;

    private void Awake()
    {
        dyingState = Animator.StringToHash("BaseLayer.Dying");
        deadBool = Animator.StringToHash("Dead");
        walkState = Animator.StringToHash("Walk");
        shoutState = Animator.StringToHash("Shout");
        speedFloat = Animator.StringToHash("Speed");
        sneakingBool = Animator.StringToHash("Sneaking");
        shoutingBool = Animator.StringToHash("Shouting");

        leftMoveState = Animator.StringToHash("Left Track");
        rightMoveState = Animator.StringToHash("Right Track");
        spinningBool = Animator.StringToHash("Spinning");
    }
}
