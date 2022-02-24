using System;
using UnityEngine;

public class EnemyAttackState : IEnemyState
{        
    private readonly IEnemyView _view;                       

    public EnemyAttackState(IEnemyView view)
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

