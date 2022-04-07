public interface IEnemyState : IState
{    
    bool CouldBeChanged { get; }
    void EnterState();              
    void ExitState();
}

