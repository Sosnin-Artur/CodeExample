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
        var follow = new EnemyFollowState(view);

        States = new List<IEnemyState>
        {
            idle, attack, follow
        };

        ChangeState(idle);
    }

    public void ChangeEnemyState(EnemyStates state)
    {
        ChangeState(States[(int)state]);
    }

    public void ChangeEnemyState(int state)
    {
        ChangeState(States[state]);
    }        
}