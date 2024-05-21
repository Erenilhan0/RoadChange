using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Car : Movable
{
    [SerializeField] private WheelCollider[] Wheels;

    [SerializeField] private float Acceleration;

    [SerializeField] private LayerMask GroundLM;
    public float MoveSpeed;
 
    public override void OnSpawn()
    {
        rb.isKinematic = false;

        foreach (var wheel in Wheels)
        {
            wheel.motorTorque = 0;
        }
    }
    
    private void FixedUpdate()
    {
        CarMove();
    }
    
    private void CarMove()
    {
        //rb.velocity = Vector3.zero;
        if (IsGrounded())
        {
            rb.AddForce(transform.forward*MoveSpeed,ForceMode.Impulse);
        }
        
        //
        // foreach (var wheel in Wheels)
        // {
        //     if (wheel.isGrounded)
        //     {
        //         wheel.motorTorque = Acceleration;
        //     }
        //     else
        //     {
        //         wheel.motorTorque = 0;
        //     }
        // }
    }

    private bool IsGrounded()
    {
        Debug.DrawRay(transform.position,-transform.up,Color.red);
        return Physics.Raycast(transform.position,-transform.up, out var hit,1,GroundLM);
    }
    public override void OnCrash()
    {
        rb.isKinematic = true;
        gameObject.SetActive(false);
    }

    // private void OnCollisionEnter(Collision collision)
    // {
    //     if (collision.collider.CompareTag("Ball"))
    //     {
    //         OnCrash();
    //     }
    // }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("DropZone"))
        {
            OnCrash();

        }
    }
   
}
