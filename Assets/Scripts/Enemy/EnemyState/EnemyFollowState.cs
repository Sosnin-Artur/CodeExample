using System;
using UnityEngine;

public class EnemyFollowState : IEnemyState
{    
    private IEnemyView _view;                   
    private Transform _target;

    private float _heightEpsilon = 0.1f;
    public EnemyStates StateType => EnemyStates.Follow;

    public EnemyFollowState(IEnemyView view, float heightEpsilon)
    {
        InitView(view);
        _target = view.Target.Value;
        _heightEpsilon = heightEpsilon;
    }
    
    public void InitView(IEnemyView view)
    {
        _view = view;        
        _view.Target.Subscribe(x => _target = x);
    }

    public void EnterState()
    {        
    }

    public void Update()
    {        
        var targetPostion = _target.position;
        var currentPosition = _view.Transform.position;

        var direction = (targetPostion - currentPosition).normalized;        
        _view.Mover.MoveInDirectionX(direction.x);

        if (direction.y > _heightEpsilon)
        {
            _view.Mover.OnJump();
        }
    }            

    public void ExitState()
    {
        _view.Mover.StopMove();
    }
}
