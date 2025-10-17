using MVP;
using System;
using System.Collections;
using UnityEngine;

public interface ISpawnerView : IView
{
    event Action EnemyCreatingEvent;
    event Action StartedEvent;
    event Action DestroedEvent;
    
    float SpawnRadious { get; }    
    Transform Transform { get; }

    IEnumerator SpawnEnemy();    
}