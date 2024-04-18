using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : Movable
{
    public float speed;
    public float maxAngSpeed;
    public float moveSpeed;
    public override void OnSpawn()
    {
        rb.isKinematic = false;
    }
    
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rb.maxAngularVelocity = maxAngSpeed;
        
        var torqueValue = new Vector3(1,0,0);
        rb.AddTorque(transform.right * (speed * Time.deltaTime),ForceMode.VelocityChange);
        
        // Vector3 fwd = transform.rotation * Vector3.forward;
        //
        // var force = new Vector3(transform.forward.x, 0, transform.forward.z);
        // transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
        // rb.AddForce(transform.forward*moveSpeed,ForceMode.VelocityChange);
        //

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
