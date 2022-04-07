using UnityEngine;

public interface IEnemyStateMachine : IStateMachine<IEnemyState>
{
    void Tick();    

    void Attack();    

    void Follow(Transform target);    

    void Idle();        
}