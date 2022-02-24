using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SpawnerView : MonoBehaviour, ISpawnerView
{
    public event Action OnEnemyCreatingEvent;
        
    [SerializeField]
    private float _spawnRadious = 2.0f;
    [SerializeField]
    private float _durationBetweenSpawn = 2.0f;
    [SerializeField]
    private int _enemysSpawnCount = 2;
    [SerializeField]
    private Transform _target;

    private Transform _transform;
    
    public float SpawnRadious => _spawnRadious;

    public Transform Target => _target;

    public Transform Transform => _transform;

    public void StartSpawner()
    {
        StartCoroutine(CastEnemy());
    }

    private void Awake()
    {
        _transform = transform;
        StartSpawner();
    }

    private IEnumerator CastEnemy()
    {        
        var waiter = new WaitForSeconds(_durationBetweenSpawn);
        var counter = 0;

        while (counter < _enemysSpawnCount)
        {                        
            OnEnemyCreatingEvent?.Invoke();
            counter++;

            yield return waiter;
        }
    }    
}
