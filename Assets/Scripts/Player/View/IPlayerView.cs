using System;
using MVP;
using UnityEngine;

public interface IPlayerView : IView
{
    event Action EnabledEvent;
    event Action DisabledEvent;
    event Action<BaseGroundItem> TakingItemEvent;

    Mover Mover { get; }
    Transform Transform { get;}
    Transform WeaponPlace { get; }
    BaseInteractableObject IntersectedInteractableObject { get; }

    void Die();
}