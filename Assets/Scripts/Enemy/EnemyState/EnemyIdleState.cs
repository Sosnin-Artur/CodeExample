using System;
using UnityEngine;

public class EnemyIdleState : IEnemyState
{        
    private IEnemyView _view;        

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

