using MVP;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

public class EnemyFactory<TAbstract, TConcrete> : IEnemyFactory<TAbstract>
    where TAbstract : BaseEnemyPresenter
    where TConcrete : class, TAbstract
{        
    private readonly GameObject _prefab;
    private readonly Transform _parent;

    private readonly IPresenterFactory<IEnemyView, TConcrete> _enemyFactory;
    private readonly IPresenterFactory<IHealthView, HealthPresenter> _healthFactory;
    
    public EnemyFactory(DiContainer container, GameObject prefab, Transform parent)
    {                          
        _prefab = prefab;
        _parent = parent;

        var subContainer = container.CreateSubContainer();
        _enemyFactory = new PresenterFactory<IEnemyView, TConcrete>(subContainer);
        _healthFactory = new PresenterFactory<IHealthView, HealthPresenter>(subContainer);        
    }

    public TAbstract Create()
    {                       
        var enemy = GameObject.Instantiate(_prefab, _parent);        
        enemy.transform.SetParent(_parent);
        
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

