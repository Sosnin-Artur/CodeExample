using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyPool : GenericObjectPool<BaseEnemyPresenter, IEnemyFactory>
{        
    public EnemyPool(IEnemyFactory enemyFactory, int count) : base(enemyFactory, count) 
    {       
    }

    public override BaseEnemyPresenter Get()
    {        
        var newObject = PooledObjects.Dequeue(); ;
        newObject.View.GameObject.SetActive(true);

        return newObject;
    }

    public override void Release(BaseEnemyPresenter objectToSet)
    {        
        objectToSet.View.GameObject.SetActive(false);
        PooledObjects.Enqueue(objectToSet);
    }       
}
