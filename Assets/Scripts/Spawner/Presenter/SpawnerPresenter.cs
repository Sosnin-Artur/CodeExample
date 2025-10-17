using ObjectPool;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SpawnerPresenter : BaseSpawnerPresenter
{
    private readonly LazyInject<GenericObjectPool<BasePoolableEnemyPresenter, 
        IEnemyFactory<BasePoolableEnemyPresenter>>> _pool;
    private readonly BaseCoroutineObject _spawnCoroutine;

    public SpawnerPresenter(
        ISpawnerView view, 
        LazyInject<GenericObjectPool<BasePoolableEnemyPresenter, 
            IEnemyFactory<BasePoolableEnemyPresenter>>> pool) : base(view)
    {                
        _pool = pool;                    
        
        _spawnCoroutine = new CoroutineWrapper(view as SpawnerView, view.SpawnEnemy);

        View.EnemyCreatingEvent += CreateEnemy;
        View.StartedEvent += _spawnCoroutine.Start;
        View.DestroedEvent += Dispose;
    }    

    public override void CreateEnemy()
    {                
        var position = (Vector2)View.Transform.position +
            Vector2.right * View.SpawnRadious * UnityEngine.Random.Range(-1.0f, 1.0f);
        
        var enemy = _pool.Value.Get();                        
        enemy.View.Transform.position = position;
    }

    public override void Dispose()
    {
        View.EnemyCreatingEvent -= CreateEnemy;
        _spawnCoroutine.Stop();
    }   
}

