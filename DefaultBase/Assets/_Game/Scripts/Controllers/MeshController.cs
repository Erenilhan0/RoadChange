using System;
using System.Collections;
using System.Collections.Generic;
using MD_Package;
using UnityEngine;
using UnityEngine.Serialization;

public class MeshController : MonoBehaviour
{
    [SerializeField] private EditableGround[] EditableGrounds;
    [SerializeField] private Transform EditableGroundParent;


    [Header("Control Point")] 
    [SerializeField] private Camera MainCam;
    [SerializeField] private LayerMask ControlPointLm;
    private ControlPoint _controlPointOnHold;

    [Header("Point Move Stats")]
    [SerializeField] private float MoveAmount;
    [SerializeField] private float MoveInterval;
    private float _currentTimer;


    private void Start()
    {
        foreach (var ground in EditableGrounds)
        {
            ground.SetMeshPoints(EditableGroundParent);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectControlPoint();
        }

        if (Input.GetMouseButton(0))
        {
            if (_controlPointOnHold != null)
            {
                ControlPointMove();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            _controlPointOnHold = null;
            _currentTimer = 0;
        }
    }

    private void SelectControlPoint()
    {
        var direction = MainCam.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(direction, out var hit, 50, ControlPointLm)) return;

        if (_controlPointOnHold == null)
        {
            _controlPointOnHold = hit.transform.GetComponent<ControlPoint>();
        }

        _currentTimer = MoveInterval;
    }

    private void ControlPointMove()
    {
        _currentTimer += Time.deltaTime;

        if (!(_currentTimer >= MoveInterval)) return;

        _currentTimer = 0;

        var mouseY = Input.GetAxis("Mouse Y");

        if (mouseY > 0)
        {
            _controlPointOnHold.ChangePosition(true, MoveAmount);
        }
        else if (mouseY < 0)
        {
            _controlPointOnHold.ChangePosition(false, MoveAmount);
        }
    }
}