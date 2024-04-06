using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{

    public int poolSize;
    
    [SerializeField] private GameObject carPrefab;

    public List<GameObject> carPool;
    public Transform carParent;

    public float spawnRate;
    private void Start()
    {
        GameManager.I.OnGamePhaseChange += OnOnGamePhaseChange;
        SpawnCarPool();
    }

    private void OnOnGamePhaseChange(GamePhase obj)
    {
        if (obj == GamePhase.Game)
        {
            StartCoroutine(OpenCarFromPool());
        }
    }

    private void SpawnCarPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            var car = Instantiate(carPrefab, transform.position, transform.rotation,carParent);
            carPool.Add(car);
        }
    }

    private IEnumerator OpenCarFromPool()
    {
        yield return new WaitForSeconds(spawnRate);

        var carFromPool = GetCarFromPool();
        carFromPool.transform.position = transform.position;
        carFromPool.transform.rotation = transform.rotation;
        carFromPool.SetActive(true);
        
        StartCoroutine(OpenCarFromPool());
    }


    private GameObject GetCarFromPool()
    {
        foreach (var car in carPool)
        {
            if (!car.activeInHierarchy)
            {
                return car;
            }
        }

        return SpawnNewCarToPool();
    }

    private GameObject SpawnNewCarToPool()
    {
       var car = Instantiate(carPrefab, transform.position,  transform.rotation,carParent);
       carPool.Add(car);
       return car;
    }
}
