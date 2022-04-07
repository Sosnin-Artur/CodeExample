using System.Collections;
using System;
using System.Reactive.Linq;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyPresenter : BaseEnemyPresenter
{
    private IEnemyModel _enemyModel;         

    public IEnemyModel Model => _enemyModel;

    public EnemyPresenter(
        IEnemyView view, 
        IEnemyModel enemyModel, 
        IHealthModel healthModel, 
        BaseEnemyStateMachine stateMachine) : base(view, stateMachine)
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

    public override void CallDeath(int currentHealth)
    {
        if (currentHealth < 0)
        {
            View.Die();
        }
    }    
}
