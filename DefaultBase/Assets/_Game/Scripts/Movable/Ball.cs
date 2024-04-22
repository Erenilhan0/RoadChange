using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Ball : Movable
{
    [SerializeField] private float Speed;
    [SerializeField] private float MaxAngSpeed;


    [SerializeField] private Transform Target;
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
        MaxAngSpeed = Speed * 1.5f;
        rb.maxAngularVelocity = MaxAngSpeed;
        rb.AddTorque(transform.right * (Speed * Time.deltaTime),ForceMode.VelocityChange);
        
        // Vector3 fwd = transform.rotation * Vector3.forward;
        //
        // var force = new Vector3(transform.forward.x, 0, transform.forward.z);
        // transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
        // rb.AddRelativeForce(transform.forward*Speed,ForceMode.Force);
      //   
      //   Target.position = transform.position + new Vector3(transform.forward.x,0,transform.forward.z);
      //
      // //  var fwd = Target.position - transform.position;
      

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
