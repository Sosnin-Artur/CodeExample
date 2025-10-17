using System;
using UnityEngine;

public class EnemyAttackState : IEnemyState
{        
    private readonly IEnemyView _view;

    private bool _couldBeChanged;
    public bool CouldBeChanged => _couldBeChanged;

    public EnemyAttackState(IEnemyView view)
    {
        _view = view;
    }    

    public void EnterState()
    {
        Debug.Log("Attack");
        _couldBeChanged = true;
    }

    public void Update()
    {
    }    

    public void ExitState()
    {
    }
}

