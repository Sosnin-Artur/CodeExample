using System;
using MVP;
public abstract class BaseHealthPresenter : BasePresenter<IHealthView>
{    
    public abstract IHealthModel Model
    {
        get;
    }

    public BaseHealthPresenter(IHealthView view) : base(view)
    {
    }
        
    public abstract void Heal(int value);
    public abstract void ApplyDamage(int value);    
}