using System.Collections;
using System;
using System.Reactive.Linq;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyPresenter : BaseEnemyPresenter
{
    private IEnemyModel _enemyModel;
    private IEnemyState _currentStateHandler;
    private EnemyStates _currentState = EnemyStates.None;
    
    private List<IEnemyState> _states;
    
    private EnemyFollowState _followState;

    public EnemyPresenter(IEnemyView view, IEnemyModel model, IHealthModel healthModel) : base(view)
    {
        _enemyModel = model;
        healthModel.CurrentHealth.Subscribe(x => CallDeath(x));

        View.OnSetTargetEvent += Follow;
        View.OnAtackEvent += Attack;
        View.OnStayEvent += Idle;
        View.OnUpdateEvent += Tick;
        
        InitStates(view);
    }    

    public override void CallDeath(int currentHealth)
    {
        if (currentHealth < 0)
        {
            View.Die();
        }
    }            

    public void ChangeState(EnemyStates state)
    {
        if (_currentState == state)
        {
            return;
        }

        _currentState = state;

        if (_currentStateHandler != null)
        {
            _currentStateHandler.ExitState();
            _currentStateHandler = null;
        }

        _currentStateHandler = _states[(int)state];
        _currentStateHandler.EnterState();        
    }

    public void Tick()
    {        
        _currentStateHandler.Update();        
    }

    public override void Dispose()
    {        
    }    

    private void InitStates(IEnemyView view)
    {
        var idle = new EnemyIdleState(view);
        var attack = new EnemyAttackState(view);        
        var follow = new EnemyFollowState(view, View.Target);
        
        _followState = follow;
        
        _states = new List<IEnemyState>
        {
            idle, attack, follow
        };
        
        ChangeState(EnemyStates.Idle);
    }

    private void Attack()
    {        
        Debug.Log("attack");
        ChangeState(EnemyStates.Attack);
    }

    private void Follow(Transform target)
    {        
        _followState._target = target;
        ChangeState(EnemyStates.Follow);        
    }

    private void Idle()
    {
        ChangeState(EnemyStates.Idle);
    }        
}
