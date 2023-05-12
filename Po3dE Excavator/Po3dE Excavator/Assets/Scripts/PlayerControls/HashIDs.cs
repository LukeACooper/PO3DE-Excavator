using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HashIDs : MonoBehaviour
{
    public int walkState;
    public int speedFloat;

    public int leftMoveState;
    public int rightMoveState;
    public int spinningBool;

    private void Awake()
    {
        walkState = Animator.StringToHash("Walk");
        speedFloat = Animator.StringToHash("Speed");

        leftMoveState = Animator.StringToHash("Left Track");
        rightMoveState = Animator.StringToHash("Right Track");
        spinningBool = Animator.StringToHash("Spinning");
    }
}
