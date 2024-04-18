using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class Spawner : MonoBehaviour
{

    public int poolSize;
    
    [SerializeField] private Movable movablePrefab;
    
    public List<Movable> movablePool;
    public Transform poolParent;

    public float spawnRate;
    
    private void Start()
    {
        GameManager.I.OnGamePhaseChange += OnOnGamePhaseChange;
        SpawnMovablePool();
    }

    private void OnOnGamePhaseChange(GamePhase obj)
    {
        if (obj == GamePhase.Game)
        {
            StartCoroutine(OpenMovableFromPool());
        }
    }

    private void SpawnMovablePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            var movable = Instantiate(movablePrefab, transform.position, transform.rotation,poolParent);
            movable.gameObject.SetActive(false);
            movablePool.Add(movable);
        }
    }

    private IEnumerator OpenMovableFromPool()
    {
        yield return new WaitForSeconds(spawnRate);

        var movable = GetMovableFromPool();
        var transform1 = movable.transform;
        
        transform1.position = transform.position;
        transform1.rotation = transform.rotation;
        
        movable.OnSpawn();
        
        movable.gameObject.SetActive(true);

        StartCoroutine(OpenMovableFromPool());
    }


    private Movable GetMovableFromPool()
    {
        foreach (var movable in movablePool)
        {
            if (!movable.gameObject.activeInHierarchy)
            {
                return movable;
            }
        }

        return SpawnNewMovableToPool();
    }

    private Movable SpawnNewMovableToPool()
    {
       var movable = Instantiate(movablePrefab, transform.position,  transform.rotation,poolParent);
       movablePool.Add(movable);
       return movable;
    }
}
