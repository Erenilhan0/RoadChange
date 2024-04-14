using System;
using System.Collections;
using System.Collections.Generic;
using MD_Package;
using UnityEngine;

public class MeshController : MonoBehaviour
{
    public MD_MeshEditorRuntime runTimeEditor;
    public MD_MeshColliderRefresher meshColliderRefresher;


    [SerializeField] private Camera mainCam;
    public bool useNonAxisEditor = true;

    public MD_MeshProEditor targetMesh;

    public int fourPackAmount;
    private void Start()
    {
        if (targetMesh.meshWorkingPoints == null || targetMesh.meshWorkingPoints.Count ==0)
        {
            targetMesh.MPE_CreatePointsEditor();

            for (int i = 0; i < targetMesh.meshWorkingPoints.Count; i++)
            {
               var go =  targetMesh.meshWorkingPoints[i].gameObject;
                // if (i% 4 == 0)
                // {
                //     fourPackAmount++;
                // }
                if (go.transform.localPosition.y < 0.35f)
                {
                    targetMesh.meshWorkingPoints[i].gameObject.SetActive(false);
                }
            }
        }
        
        //targetMesh.meshAnimationMode = true;
        

        //
        // if (runTimeEditor.isAxisEditor)
        // {
        //     runTimeEditor.AXIS_CreateAxisHandleAutomatically();
        //     runTimeEditor.axis_targetMeshProEditor = targetMesh;
        // }

    }

    
    public void UpdateMeshCollider()
    {
        meshColliderRefresher.MeshCollider_UpdateMeshCollider();

    }
}