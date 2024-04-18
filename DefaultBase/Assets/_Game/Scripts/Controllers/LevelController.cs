using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{

    [SerializeField] private MeshController _meshController;
    public void SetLevel()
    {
        _meshController.SetMeshes();
    }
}