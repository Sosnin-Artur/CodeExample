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
        if (_view.Target.Value)
        {
            ChangeEnemyState(EnemyStates.Follow);
        }
        else
        {
            ChangeEnemyState(EnemyStates.Idle);
        }

        CurrentStateHandler.Update();
    }

    public override void ChangeState(IEnemyState state)
    {        
        if ((CurrentStateHandler != null && !CurrentStateHandler.CouldBeChanged) ||
            (CurrentStateHandler != null && CurrentStateHandler.Equals(state)))
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
