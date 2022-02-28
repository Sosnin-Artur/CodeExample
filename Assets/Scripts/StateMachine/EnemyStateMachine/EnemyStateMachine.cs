using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : BaseEnemyStateMachine
{   
    private readonly IEnemyView _view;
    private readonly IEnemyModel _model;

    public EnemyStateMachine(IEnemyView view, IEnemyModel model)        
    {        
        _view = view;
        _model = model;
    }

    public override void Tick()
    {
        CurrentStateHandler.Update();

        if (Vector2.Distance(
            _view.Transform.position,
            _model.Target.Value.position) < _view.FollowDistance)
        {
            ChangeEnemyState(EnemyStates.Follow);
        }
        else
        {
            ChangeEnemyState(EnemyStates.Idle);
        }
    }

    public override void ChangeState(IEnemyState state)
    {
        if (CurrentStateHandler == state)
        {
            return;
        }

        if (CurrentStateHandler != null)
        {
            CurrentStateHandler.ExitState();
            CurrentStateHandler = null;
        }

        CurrentStateHandler = state;
        CurrentStateHandler.EnterState();
    }    
}
