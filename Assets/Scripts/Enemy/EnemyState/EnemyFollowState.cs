using System;
using UnityEngine;

public class EnemyFollowState : IEnemyState
{    
    private readonly IEnemyView _view;        
    
    public Transform Target { get; set; }   

    public EnemyFollowState(IEnemyView view, Transform target)
    {
        _view = view;
        Target = target;
    }
    

    public void EnterState()
    {                
    }    

    public void Update()
    {
        var targetPostion = Target.position;
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


