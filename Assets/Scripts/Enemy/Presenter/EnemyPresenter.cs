using System.Collections;
using System;
using System.Reactive.Linq;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyPresenter : BaseEnemyPresenter, ITickable
{
    private readonly IEnemyModel _enemyModel;
    private readonly IEnemyStateMachine _stateMachine;
        
    private readonly LazyInject<EnemyPool> _pool;

    public EnemyPresenter
        (IEnemyView view, 
        IEnemyModel model, 
        IHealthModel healthModel, 
        IEnemyStateMachine stateMachine, 
        LazyInject<EnemyPool> pool) : base(view)
    {                
        _enemyModel = model;
        
        healthModel.CurrentHealth.Subscribe(x => CallDeath(x));
       
        _stateMachine = stateMachine;
        _pool = pool;
        
        InitViewEvents();        
    }        

    public override void CallDeath(int currentHealth)
    {
        if (currentHealth < 0)
        {
            _pool.Value.Release(this);
            View.Die();            
        }
    }            
   
    public override void Dispose()
    {        
        View.OnAtackEvent -= _stateMachine.Attack;        
        View.OnUpdateEvent -= Tick;
    }
    
    public void Tick()
    {
        _stateMachine.Tick();
        
        if (View.Target != null 
            && Vector3.Distance(
                View.Transform.position
                , View.Target.position) < View.FollowDistance)
        {
            _stateMachine.Follow(View.Target);
        }
        else
        {
            _stateMachine.Idle();
        }
    }

    private void InitViewEvents()
    {        
        View.OnAtackEvent += _stateMachine.Attack;        
        View.OnUpdateEvent += Tick;
    }    
}
