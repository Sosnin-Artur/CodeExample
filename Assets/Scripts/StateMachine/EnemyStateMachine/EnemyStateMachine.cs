using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : BaseEnemyStateMachine
{   
    private readonly IEnemyView _view;    

    public EnemyStateMachine(IEnemyView view)        
    {        
        _view = view;                
    }

    public override void Tick()
    {
        base.Tick();

        if (Vector2.Distance(
            _view.Transform.position,
            _view.Target.Value.position) < _view.FollowDistance)
        {
            ChangeState(EnemyStates.Follow);
        }
        else
        {
            ChangeState(EnemyStates.Idle);
        }
    }    
}
