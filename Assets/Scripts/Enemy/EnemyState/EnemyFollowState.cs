using System;
using UnityEngine;

public class EnemyFollowState : IEnemyState
{    
    private readonly IEnemyView _view;                   

    private Transform _target;

    public bool CouldBeChanged => true;
    
    public EnemyFollowState(IEnemyView view)
    {
        _view = view;
        _target = view.Target.Value;
                
        view.Target.Subscribe(x => _target = x);
    }
    
    public void EnterState()
    {        
    }

    public void Update()
    {
        var targetPostion = _target.position;
        var currentPosition = _view.Transform.position;

        var direction = targetPostion - currentPosition;
        _view.Mover.MoveInDirectionX(direction.x);

        if (direction.y > 0)
        {
            _view.Mover.OnJump();
        }
    }            

    public void ExitState()
    {
        _view.Mover.StopMove();
    }
}


