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
            .Bind<EnemyPool>()
            .AsSingle();

        Container
            .Bind<IEnemyView>()
            .To<EnemyView>()
            .FromComponentsInNewPrefab(_enemyPrefab)
            .UnderTransform(this.transform)
            .AsTransient();
        
        Container
            .BindInterfacesAndSelfTo<EnemyModel>()
            .AsTransient();

        Container
            .BindInterfacesAndSelfTo<HealthModel>()
            .AsTransient()
            .WhenInjectedInto<EnemyPresenter>();        

        Container
            .Bind<BaseEnemyPresenter>()            
            .To<EnemyPresenter>()            
            //.FromSubContainerResolve()            
            //.ByInstaller<EnemyInstaller>()            
            .AsTransient();              
    }    
}
