using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HashIDs : MonoBehaviour
{
    public int walkState;
    public int speedFloat;
    public int leftMoveFloat;
    public int rightMoveFloat;
    public int spinningBool;
    public int drivingLeftBool;
    public int drivingRightBool;
    

    private void Awake()
    {
        walkState = Animator.StringToHash("Walk");
        speedFloat = Animator.StringToHash("Speed");
        drivingLeftBool = Animator.StringToHash("DrivingLeft");
        drivingLeftBool = Animator.StringToHash("DrivingRight");
        leftMoveFloat = Animator.StringToHash("LeftTrack");
        rightMoveFloat = Animator.StringToHash("RightTrack");
        spinningBool = Animator.StringToHash("Spinning");
        
    }
}
