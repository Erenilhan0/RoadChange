using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public bool controlPointOnHold = true;
    public float rotateSpeed;

    private void Start()
    {
        GameManager.I.OnGamePhaseChange += OnOnGamePhaseChange;
    }

    private void OnOnGamePhaseChange(GamePhase obj)
    {
        if (obj == GamePhase.Game)
        {
            Invoke(nameof(CanRotate),1);
        }
    }

    public void CanRotate()
    {
        controlPointOnHold = false;
    }

    private void FixedUpdate()
    {
        if (GameManager.I.currentGamePhase != GamePhase.Game) return;
        
        
        if (Input.GetMouseButton(0))
        {
            RotateCamera();
        }
    }

    private void RotateCamera()
    {
        if (controlPointOnHold) return;

        transform.eulerAngles += rotateSpeed * Time.deltaTime *
                                 new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
    }
}