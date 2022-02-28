using System.Collections.Generic;

public abstract class BaseStateMachine<T> where T : IState
{
    protected T CurrentStateHandler;
    protected List<T> States;
    
    public abstract void Tick();
    
    public abstract void ChangeState(T state);    
}