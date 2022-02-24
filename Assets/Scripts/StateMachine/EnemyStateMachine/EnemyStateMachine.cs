using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : IEnemyStateMachine
{
    private IEnemyState _currentStateHandler;    

    private List<IEnemyState> _states;

    private EnemyFollowState _followState;

    private IEnemyView _view;

    public EnemyStateMachine(IEnemyView view)
    {
        _view = view;

        InitStates(view);
    }

    public void Tick()
    {
        _currentStateHandler.Update();
    }

    public void Attack()
    {
        Debug.Log("attack");
        ChangeState(_states[(int)EnemyStates.Attack]);
    }

    public void Follow(Transform target)
    {
        _followState.Target = target;
        ChangeState(_states[(int)EnemyStates.Follow]);
    }

    public void Idle()
    {
        ChangeState(_states[(int)EnemyStates.Idle]);
    }

    public void ChangeState(IEnemyState state)
    {
        if (_currentStateHandler == state)
        {
            return;
        }        

        if (_currentStateHandler != null)
        {
            _currentStateHandler.ExitState();
            _currentStateHandler = null;
        }

        _currentStateHandler = state;
        _currentStateHandler.EnterState();
    }

    private void InitStates(IEnemyView view)
    {
        var idle = new EnemyIdleState(view);
        var attack = new EnemyAttackState(view);
        var follow = new EnemyFollowState(view, _view.Target);

        _followState = follow;

        _states = new List<IEnemyState>
        {
            idle, attack, follow
        };

        ChangeState(_states[(int)EnemyStates.Idle]);
    }
}
