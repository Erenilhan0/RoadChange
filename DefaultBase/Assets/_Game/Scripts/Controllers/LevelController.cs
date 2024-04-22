using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{

    [SerializeField] private MeshController _meshController;

    [SerializeField] private Spawner[] Spawners;
    
    
    private void Start()
    {
        GameManager.I.OnGamePhaseChange += OnOnGamePhaseChange;
        
        SetLevel();
    }

    private void OnOnGamePhaseChange(GamePhase obj)
    {
        if (obj == GamePhase.Game)
        {
            LevelStarted();
        }
    }

    public void SetLevel()
    {
        foreach (var spawner in Spawners)
        {
            spawner.SpawnMovablePool();
        }
    }

    public void LevelStarted()
    {
        foreach (var spawner in Spawners)
        {
            StartCoroutine(spawner.OpenMovableFromPool());
        }
    }


    public void ResetMovables()
    {
        foreach (var spawner in Spawners)
        {
            spawner.CloseMovables();
        }
    }
}