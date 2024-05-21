using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MD_Package;
using UnityEngine;
using UnityEngine.Serialization;

public class EditableGround : MonoBehaviour
{
    [SerializeField] private MD_MeshColliderRefresher MeshColliderRefresher;

    [SerializeField] private MD_MeshProEditor MeshProEditor;

    [SerializeField] private Point[] Points;

    [SerializeField] private List<ControlPoint> ControlPoints;

    public void SetMeshPoints(Transform parent)
    {
        if (MeshProEditor.meshWorkingPoints == null || MeshProEditor.meshWorkingPoints.Count == 0)
        {
            MeshProEditor.MPE_CreatePointsEditor();
        }

        MeshProEditor.transform.SetParent(parent);

        for (int i = 0; i < MeshProEditor.meshWorkingPoints.Count; i++)
        {
            var point = MeshProEditor.meshWorkingPoints[i].gameObject;
            point.name = i.ToString();

            point.SetActive(false);

            foreach (var t in Points)
            {
                if (t.OpenIndex != i) continue;

                point.SetActive(true);
                
                ControlPoints.Add(point.GetComponent<ControlPoint>());
                
                foreach (var pointIndex in t.GroupIndexes)
                {
                    var pointToGroup = MeshProEditor.meshWorkingPoints[pointIndex].transform;
                    point.GetComponent<ControlPoint>().SetGroupParent(pointToGroup);
                }
            }
        }

        UpdateMeshCollider();
    }


    public void UpdateMeshCollider()
    {
        MeshColliderRefresher.refreshType = MD_MeshColliderRefresher.RefreshType.Once;
        MeshColliderRefresher.colliderOffset = Vector3.down * 0.1f;
        MeshColliderRefresher.MeshCollider_UpdateMeshCollider();
    }


    public void ResetControlPoints()
    {
        foreach (var controlPoint in ControlPoints)
        {
            controlPoint.ResetPosition();
        }
    }
}

[Serializable]
public class Point
{
    public int OpenIndex;
    public int[] GroupIndexes;
}