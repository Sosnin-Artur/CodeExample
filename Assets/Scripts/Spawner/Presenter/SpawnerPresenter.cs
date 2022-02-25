using System;
using System.Collections.Generic;
using UnityEngine;

class SpawnerPresenter : BaseSpawnerPresenter
{
    private readonly GenericObjectPool<BaseEnemyPresenter, IEnemyFactory> _pool;

    public SpawnerPresenter(
        ISpawnerView view, 
        GenericObjectPool<BaseEnemyPresenter, IEnemyFactory> pool) : base(view)
    {                
        _pool = pool;

        View.OnEnemyCreatingEvent += CreateEnemy;
    }    

    public override void CreateEnemy()
    {        
        var position = (Vector2)View.Transform.position +
            Vector2.right * View.SpawnRadious * UnityEngine.Random.Range(-1, 1);
        
        var enemy = _pool.Get();        
        enemy.View.Target = View.Target;
        enemy.View.Transform.position = position;
    }

    public override void Dispose()
    {
        View.OnEnemyCreatingEvent -= CreateEnemy;
    }
}

