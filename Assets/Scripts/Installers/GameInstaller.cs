using MVP;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField]
    private int _enemyInPool;    
    [SerializeField]
    private GameObject _enemyPrefab;

    public override void InstallBindings()
    {        
        Container
            .BindInstance<int>(_enemyInPool)
            .AsSingle();        
        
        Container
            .Bind<IEnemyFactory>()
            .To<EnemyFactory>()
            .AsSingle();        
        
        Container
            .Bind<GenericObjectPool<BaseEnemyPresenter, IEnemyFactory>>()
            .To<EnemyPool>()
            .AsSingle();

        Container
            .BindInstance<GameObject>(_enemyPrefab)
            .AsSingle();        
    }        
}
