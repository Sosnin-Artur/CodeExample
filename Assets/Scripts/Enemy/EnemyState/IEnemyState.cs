public interface IEnemyState
{    
    void EnterState();    
    void Update();        
    void ExitState();
}

public enum EnemyStates
{
    Idle,
    Attack,
    Follow,
    None
}

