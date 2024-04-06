using System;
using System.Collections;
using System.Collections.Generic;
using MD_Package;
using UnityEngine;

public class MeshController : MonoBehaviour
{
    public MD_MeshEditorRuntime runTimeEditor;


    [SerializeField] private Camera mainCam;
    public bool useNonAxisEditor = true;

    public MD_MeshProEditor targetMesh;


    private void Start()
    {
        if (targetMesh.meshWorkingPoints == null || targetMesh.meshWorkingPoints.Count ==0)
        {
            targetMesh.MPE_CreatePointsEditor();

            for (int i = 0; i < targetMesh.meshWorkingPoints.Count; i++)
            {
                targetMesh.meshWorkingPoints[i].gameObject.layer = 7;
                // if (i > 15)
                // {
                //     targetMesh.meshWorkingPoints[i].gameObject.SetActive(false);
                // }
            }
        }
        
        //targetMesh.meshAnimationMode = true;
        


        if (runTimeEditor.isAxisEditor)
        {
            runTimeEditor.AXIS_CreateAxisHandleAutomatically();
            runTimeEditor.axis_targetMeshProEditor = targetMesh;
        }

    }


    public void UpdateMeshCollider()
    {
        
        
    }
}