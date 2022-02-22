using System;
using UnityEngine;

public class EnemyFollowState : IEnemyState
{    
    private IEnemyView _view;        
    public Transform _target;        

    public EnemyFollowState(IEnemyView view, Transform target)
    {
        _view = view;
        _target = target;
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


