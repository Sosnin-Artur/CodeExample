using MVP;
using UnityEngine;

public abstract class BaseEnemyPresenter : BasePresenter<IEnemyView>
{
    protected BaseEnemyStateMachine StateMachine;

    public BaseEnemyPresenter(IEnemyView view, BaseEnemyStateMachine stateMachine) : base(view)
    {
        StateMachine = stateMachine;
    }      

    public void InitStateMachine(BaseEnemyStateMachine stateMachine)
    {
        stateMachine.InitStates(View);
        stateMachine.ChangeEnemyState(EnemyStates.Idle);
    }

    public void InitViewEvents()
    {
        View.AtackingEvent += StateMachine.ChangeEnemyState;
        View.UpdatingEvent += StateMachine.Tick;
    }

    public abstract void CallDeath(int currentHealth);
    
    public override void Dispose()
    {
        View.AtackingEvent -= StateMachine.ChangeEnemyState;
        View.UpdatingEvent -= StateMachine.Tick;
    }
}
