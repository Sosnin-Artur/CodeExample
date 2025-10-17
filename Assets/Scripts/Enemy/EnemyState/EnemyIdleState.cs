using System;
using UnityEngine;

public class EnemyIdleState : IEnemyState
{        
    private readonly IEnemyView _view;

    public bool CouldBeChanged => true;

    public EnemyIdleState(IEnemyView view)
    {        
        _view = view;        
    }

    public void EnterState()
    {           
    }    

    public void Update()
    {
        
    }    

    public void ExitState()
    {
    }
}

