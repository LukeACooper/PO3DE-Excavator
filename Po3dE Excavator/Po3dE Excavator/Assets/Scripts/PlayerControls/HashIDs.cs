using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HashIDs : MonoBehaviour
{
    public int walkState;
    public int speedFloat;
    public int leftMoveFloat;
    public int rightMoveFloat;
    public int spinningFloat;
    public int drivingBool;
    public int rotatingFloat;

    private void Awake()
    {
        walkState = Animator.StringToHash("Walk");
        speedFloat = Animator.StringToHash("Speed");
        drivingBool = Animator.StringToHash("Driving");
        leftMoveFloat = Animator.StringToHash("LeftTrack");
        rightMoveFloat = Animator.StringToHash("RightTrack");
        spinningFloat = Animator.StringToHash("Spinning");
        rotatingFloat = Animator.StringToHash("Rotating");
    }
}
