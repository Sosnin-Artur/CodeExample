using System.Collections.Generic;
using UnityEngine;

public abstract class BaseStateMachine<T> where T : IState
{
    protected T CurrentStateHandler;
    protected List<T> States;
    
    public T CurrentState => CurrentStateHandler;

    public abstract void Tick();
    
    public virtual void ChangeState(T state)
    {
        if (CurrentStateHandler == null || CurrentStateHandler.Equals(state))
        {            
            return;
        }
    }

}