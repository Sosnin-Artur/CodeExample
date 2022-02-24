using MVP;
using UnityEngine;

public abstract class BaseEnemyPresenter : BasePresenter<IEnemyView>
{
    public BaseEnemyPresenter(IEnemyView view) : base(view)
    {
    }

    public abstract void CallDeath(int currentHealth);        
}