using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private MeshController MeshController;
    [SerializeField] private Spawner[] Spawners;

    public DoorController [] DoorControllers;
    
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

    private void LevelStarted()
    {
        foreach (var spawner in Spawners)
        {
            StartCoroutine(spawner.OpenMovableFromPool());
        }
    }

    public void ResetLevel()
    {
        MeshController.ResetGrounds();
        ResetMovables();
    }

    public void ResetMovables()
    {
        foreach (var spawner in Spawners)
        {
            spawner.CloseMovables();
        }
    }
}