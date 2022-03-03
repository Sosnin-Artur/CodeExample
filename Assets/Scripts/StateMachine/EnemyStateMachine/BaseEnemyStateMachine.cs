using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemyStateMachine : BaseStateMachine<IEnemyState>
{                  
    public BaseEnemyStateMachine()
    {                
    }
    
    public virtual void InitStates(IEnemyView view)
    {
        var idle = new EnemyIdleState(view);
        var attack = new EnemyAttackState(view);
        var follow = new EnemyFollowState(view, view.HeigthEpsilon);

        States = new List<IEnemyState>
        {
            idle, attack, follow
        };

        ChangeState(idle);
    }

    public virtual void EnterState(IEnemyState state)
    {
        CurrentStateHandler = state;
        CurrentStateHandler.EnterState();
    }

    public void ChangeState(EnemyStates newState)
    {
        foreach (var state in States)
        {
            if (newState == state.StateType)
            {
                ChangeState(state);
                break;
            }
        }
    }

    public override bool ChangeState(IEnemyState state)
    {        
        if (!base.ChangeState(state))
        {
            return false;
        }

        if (CurrentStateHandler != null)
        {
            ExitState();
        }

        EnterState(state);

        return true;
    }    

    public void ExitState()
    {
        CurrentStateHandler.ExitState();
        CurrentStateHandler = null;
    }
}