using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Ball : Movable
{
    [SerializeField] private float Speed;
    [SerializeField] private float MaxAngSpeed;

    public override void OnSpawn()
    {
        rb.isKinematic = false;
    }
    
    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        MaxAngSpeed = Speed * 1.5f;
        rb.maxAngularVelocity = MaxAngSpeed;
        rb.AddTorque(transform.right * (Speed * Time.deltaTime),ForceMode.VelocityChange);
        
    }
    
    public override void OnCrash()
    {
        gameObject.SetActive(false);
        rb.isKinematic = true;
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
            OnCrash();
        }
    }
}
