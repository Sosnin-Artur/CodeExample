using MVP;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

class EnemyFactory : IEnemyFactory
{        
    private readonly GameObject _prefab;
    
    private readonly IPresenterFactory<IEnemyView, EnemyPresenter> _enemyFactory;
    private readonly IPresenterFactory<IHealthView, HealthPresenter> _healthFactory;

    public EnemyFactory(GameObject prefab, DiContainer container)
    {                          
        _prefab = prefab;

        var subContainer = container.CreateSubContainer();
        _enemyFactory = new PresenterFactory<IEnemyView, EnemyPresenter>(subContainer);
        _healthFactory = new PresenterFactory<IHealthView, HealthPresenter>(subContainer);
    }

    public BaseEnemyPresenter Create()
    {                       
        var enemy = GameObject.Instantiate(_prefab);        
        
        _healthFactory.BindParamsAndCreate(
            enemy, 
            typeof(HealthView), 
            typeof(HealthModel));
        
        var res = _enemyFactory.BindParamsAndCreate(
            enemy, 
            typeof(EnemyView), 
            typeof(EnemyModel), 
            typeof(EnemyStateMachine));

        _healthFactory.UnbindPresenter();
        _enemyFactory.UnbindPresenter();        
        
        return res;
    }   
}

