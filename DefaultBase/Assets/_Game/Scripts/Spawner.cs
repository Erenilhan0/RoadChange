using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Movable MovablePrefab;
    [SerializeField] private List<Movable> MovablePool;

    [SerializeField] private int PoolSize;
    [SerializeField] private Transform PoolParent;

    [SerializeField] private float SpawnRate;

    
    public void SpawnMovablePool()
    {
        for (int i = 0; i < PoolSize; i++)
        {
            var movable = Instantiate(MovablePrefab, transform.position, transform.rotation, PoolParent);
            movable.gameObject.SetActive(false);
            MovablePool.Add(movable);
        }
    }

    public IEnumerator OpenMovableFromPool()
    {
        yield return new WaitForSeconds(SpawnRate);

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
        foreach (var movable in MovablePool)
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
        var transform1 = transform;
        var movable = Instantiate(MovablePrefab, transform1.position, transform1.rotation, PoolParent);
        MovablePool.Add(movable);
        return movable;
    }


    public void CloseMovables()
    {
        foreach (var movable in MovablePool)
        {
           movable.gameObject.SetActive(false);
        }
    }
}