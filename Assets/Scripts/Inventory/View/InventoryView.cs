using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InventoryView : MonoBehaviour, IInventoryView
{
    public event Action OnEnabledEvent;
    public event Action OnDisabledEvent;

    [SerializeField]
    private int _inventorySize;
    [SerializeField]
    private Transform _container;
    [SerializeField]
    private Transform _draggingParent;    
    [SerializeField]
    private ItemEjector _ejector;

    public int InventorySize => _inventorySize;
    
    public Transform DraggingParent => _draggingParent;

    public Transform Container => _container;

    public ItemEjector Ejector => _ejector;

    private void Awake()
    {        
        OnEnabledEvent?.Invoke();        
    }
    
    private void OnDisable()
    {        
        OnDisabledEvent?.Invoke();
    }
}
