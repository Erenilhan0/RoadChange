using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    public float rotateSpeed;

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            RotateCamera();
        }
    }

    private void RotateCamera()
    {
        transform.eulerAngles += rotateSpeed * new Vector3(0, Input.GetAxis("Mouse X"), 0);
    }
}
