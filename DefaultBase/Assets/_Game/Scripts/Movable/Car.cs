using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Car : Movable
{
    [SerializeField] private WheelCollider[] Wheels;

    [SerializeField] private float Acceleration;

    
    public float MoveSpeed;
 
    public override void OnSpawn()
    {
        rb.velocity = Vector3.zero;
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
        rb.AddForce(transform.forward*MoveSpeed,ForceMode.VelocityChange);
        foreach (var wheel in Wheels)
        {
            if (wheel.isGrounded)
            {
                wheel.motorTorque = Acceleration;
            }
            else
            {
                wheel.motorTorque = 0;
            }
        }
    }

    public override void OnCrash()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ball"))
        {
            OnCrash();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("DropZone"))
        {
            gameObject.SetActive(false);
        }
    }
   
}
