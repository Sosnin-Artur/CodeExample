using System.Collections.Generic;

public abstract class BaseStateMachine<T> where T : class, IState
{
    protected T CurrentStateHandler;
    protected List<T> States;
    
    public virtual void Tick()
    {
        CurrentStateHandler.Update();
    }
    
    public virtual bool ChangeState(T state)
    {
        if (state == CurrentStateHandler)
        {            
            return false;
        }

        return true;
    }

}