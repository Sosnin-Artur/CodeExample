using System;

public abstract class BaseHealthPresenter : BasePresenter<IHealthView>
{
    public event Action OnDeathEvent;

    public BaseHealthPresenter(IHealthView view) : base(view)
    {
    }
    
    public abstract void InitModel(int currentHealth, int maxHealth);
    public abstract void Heal(int value);
    public abstract void TakeDamage(int value);

    protected void InvokeDeathEvent()
    {
        OnDeathEvent?.Invoke();
    }
}