public interface IEnemyState : IState
{    
    EnemyStates StateType {  get; }
    void EnterState();              
    void ExitState();
}

