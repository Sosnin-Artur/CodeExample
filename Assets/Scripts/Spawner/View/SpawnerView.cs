using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SpawnerView : MonoBehaviour, ISpawnerView
{
    public event Action EnemyCreatingEvent;
    public event Action StartedEvent;
    public event Action DestroedEvent;

    [SerializeField]
    private float _spawnRadious = 2.0f;
    [SerializeField]
    private float _durationBetweenSpawn = 2.0f;
    [SerializeField]
    private int _enemysSpawnCount = 2;    

    private Transform _transform;
    
    public float SpawnRadious => _spawnRadious;
    
    public Transform Transform => _transform;

    public void Awake()
    {
        _transform = transform;
    }

    public void Start()
    {
        StartedEvent?.Invoke();
    }

    public IEnumerator SpawnEnemy()
    {        
        var waiter = new WaitForSeconds(_durationBetweenSpawn);
        var counter = 0;

        while (counter < _enemysSpawnCount)
        {
            yield return waiter;

            EnemyCreatingEvent?.Invoke();
            counter++;            
        }
    }
    
    private void OnDestroy()
    {
        DestroedEvent?.Invoke();
    }
}
