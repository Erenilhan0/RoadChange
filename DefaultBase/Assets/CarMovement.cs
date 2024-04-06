using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Vector3 = System.Numerics.Vector3;

public class CarMovement : MonoBehaviour
{
    [SerializeField] private WheelCollider[] Wheels;

    [SerializeField] private float Acceleration;

    [SerializeField] private Rigidbody rb;
    public float moveSpeed;
    public LayerMask groundLayerMask;
    void FixedUpdate()
    {
        CarMove();
    }


    private void CarMove()
    {
        // if (IsGrounded())
        // {
        //     rb.AddForce(transform.forward*moveSpeed,ForceMode.Force);
        // }
        
        foreach (var wheel in Wheels)
        {
            if (wheel.isGrounded)
            {
                wheel.motorTorque = Acceleration;
            }
        }
    }

    public bool IsGrounded()
    {
        Debug.DrawRay(transform.position, -transform.up, Color.red);
        return Physics.Raycast(transform.position, -transform.up, 1,groundLayerMask);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DropZone"))
        {
            gameObject.SetActive(false);
        }
    }
}
