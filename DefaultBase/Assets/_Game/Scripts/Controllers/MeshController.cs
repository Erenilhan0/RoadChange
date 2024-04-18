using System;
using System.Collections;
using System.Collections.Generic;
using MD_Package;
using UnityEngine;
using UnityEngine.Serialization;

public class MeshController : MonoBehaviour
{
    public MD_MeshColliderRefresher meshColliderRefresher;

    public MD_MeshProEditor[] editableGrounds;
    [SerializeField] private Transform editableGroundParent;
    public LayerMask controlPointLM;

    [SerializeField] private Camera mainCam;

    public ControlPoint controlPointOnHold;

    public float moveAmount;

    [SerializeField] private float moveInterval;
    private float currentTimer;


   
    private void Start()
    {
        foreach (var ground in editableGrounds)
        {
            if (ground.meshWorkingPoints != null && ground.meshWorkingPoints.Count != 0) continue;

            ground.MPE_CreatePointsEditor();

            ground.transform.SetParent(editableGroundParent);

            foreach (var point in ground.meshWorkingPoints)
            {
                var go = point.gameObject;

                if (go.transform.localPosition.y < 0.35f)
                {
                    point.gameObject.SetActive(false);
                }
            }
        }
    }

    public void SetMeshes()
    {
        foreach (var ground in editableGrounds)
        {
            if (ground.meshWorkingPoints != null && ground.meshWorkingPoints.Count != 0) continue;

            ground.MPE_CreatePointsEditor();

            ground.transform.SetParent(editableGroundParent);

            foreach (var point in ground.meshWorkingPoints)
            {
                var go = point.gameObject;

                if (go.transform.localPosition.y < 0.35f)
                {
                    point.gameObject.SetActive(false);
                }
            }
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
            if (controlPointOnHold != null)
            {
                ControlPointMove();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            controlPointOnHold = null;
            currentTimer = 0;
        }
    }

    private void SelectControlPoint()
    {
        var direction = mainCam.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(direction, out var hit, 50, controlPointLM)) return;

        if (controlPointOnHold == null)
        {
            controlPointOnHold = hit.transform.GetComponent<ControlPoint>();
        }

        currentTimer = moveInterval;
    }

    private void ControlPointMove()
    {
        currentTimer += Time.deltaTime;

        if (!(currentTimer >= moveInterval)) return;

        currentTimer = 0;

        var mouseY = Input.GetAxis("Mouse Y");

        if (mouseY > 0)
        {
            controlPointOnHold.ChangePosition(true, moveAmount);
        }
        else if (mouseY < 0)
        {
            controlPointOnHold.ChangePosition(false, moveAmount);
        }


        //  UpdateMeshCollider();
    }

    public void UpdateMeshCollider()
    {
        meshColliderRefresher.MeshCollider_UpdateMeshCollider();
    }
}