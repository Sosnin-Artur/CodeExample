using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using ObjectPool;


public class EnemyPool : GenericObjectPool<
    BasePoolableEnemyPresenter, 
    IEnemyFactory<BasePoolableEnemyPresenter>>
{
    public EnemyPool(IEnemyFactory<BasePoolableEnemyPresenter> enemyFactory, int count) : 
        base(enemyFactory, count)
    {
    }    
}
