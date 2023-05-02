using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target;

    private void LateUpdate()
    {
        transform.LookAt(target.transform);
    }
}
