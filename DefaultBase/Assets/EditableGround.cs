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

    [SerializeField] private int [] OpenPointIndexes;
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

            point.SetActive(OpenPointIndexes.Contains(i));
        }
        
    }

    
    
    public void UpdateMeshCollider()
    {
        MeshColliderRefresher.MeshCollider_UpdateMeshCollider();
    }
}