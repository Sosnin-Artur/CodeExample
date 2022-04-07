using MVP;
using System;
using UnityEngine;

public interface IEnemyView : IView
{    
    event Action<EnemyStates> AtackingEvent;    
    event Action UpdatingEvent;
    
    Mover Mover { get; }    
    Transform Transform { get; }
    ReactiveProperty<Transform> Target { get; }
    GameObject GameObject { get; }
    float FollowDistance { get; }
    
    void Die();
}