using System.Collections;
using System;
using System.Reactive.Linq;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using ObjectPool;

public class PoolableEnemyPresenter : BasePoolableEnemyPresenter
{
    private IEnemyModel _enemyModel;
    public IEnemyModel Model => _enemyModel;

    public PoolableEnemyPresenter(
        IEnemyView view, 
        IEnemyModel enemyModel, 
        IHealthModel healthModel, 
        BaseEnemyStateMachine stateMachine, 
        LazyInject<GenericObjectPool<
                    BasePoolableEnemyPresenter,
                    IEnemyFactory<BasePoolableEnemyPresenter>>> pool) : 
        base(view, stateMachine, pool)
    {
        InitModels(view, enemyModel, healthModel);
        InitStateMachine(stateMachine);
        InitViewEvents();
    }

    public void InitModels(
        IEnemyView view,
        IEnemyModel enemyModel,
        IHealthModel healthModel)
    {
        _enemyModel = enemyModel;
        _enemyModel.Target = new ReactiveProperty<Transform>(view.Target.Value);

        healthModel.CurrentHealth.Subscribe(x => CallDeath(x));
    }    

    public override void Spawn()
    {
        View.GameObject.SetActive(true);
    }        

    public override void Release()
    {
        View.GameObject.SetActive(false);
    }

    public override void Dispose()
    {
        base.Dispose();
        Pool.Value.Release(this);
    }
       
    public override void CallDeath(int currentHealth)
    {
        if (currentHealth <= 0)
        {
            View.Die();
            Pool.Value.Release(this);            
        }
    }        
}
