using MVP;
using System;
using UnityEngine;

public interface IEnemyView : IView
{
    event Action<Transform> OnSetTargetEvent;
    event Action OnAtackEvent;
    event Action OnStayEvent;
    event Action OnUpdateEvent;
    
    Mover Mover { get; }    
    Transform Transform { get; }    
    Transform Target { get; set; }
    public GameObject GameObject { get; }
    float FollowDistance { get; }  
    
    void Die();
}