using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour, IPlayerView
{            
    public event Action EnabledEvent;
    public event Action DisabledEvent;
    public event Action<BaseGroundItem> TakingItemEvent;

    [SerializeField]
    [Tooltip("Movement component")]
    private Mover _mover;
    [SerializeField]        
    private Transform _transform;
    [SerializeField]
    private Transform _weaponPlace;

    private BaseInteractableObject _interactableObject;
    public Mover Mover => _mover;

    public Transform Transform => _transform;
    public Transform WeaponPlace => _weaponPlace;

    public BaseInteractableObject IntersectedInteractableObject => _interactableObject;

    public void OnEnable()
    {
        EnabledEvent?.Invoke();
    }

    public void Die()
    {
        Debug.Log("Player death");
    }            
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        var item = other.GetComponent<BaseGroundItem>();
        
        if (item)
        {            
            TakingItemEvent?.Invoke(item);            
        }

        var interactable = other.GetComponent<BaseInteractableObject>();        
        
        if (interactable)
        {
            _interactableObject = interactable;            
        }        
    }

    private void OnDisable()
    {
        DisabledEvent?.Invoke();
    }
}

