using MVP;
using System;
using UnityEngine;

public interface ISpawnerView : IView
{
    event Action OnEnemyCreatingEvent;

    float SpawnRadious { get; }
    Transform Target { get; }
    Transform Transform { get; }

    void StartSpawner();    
}