using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InventoryView : MonoBehaviour, IInventoryView
{
    public event Action EnabledEvent;    
    public event Action DisabledEvent;
    public event Action<bool> SwitchedActivatorEvent;
    public event Action<BaseItemObject, bool> DraggedItemEvent;

    [SerializeField]
    private int _inventorySize = 3;
    [SerializeField]
    private List<BaseItemObject> _startItems;
    [SerializeField]
    private string _savePath = "default";
    [SerializeField]
    private Transform _container;
    [SerializeField]
    private Transform _draggingParent;    
    [SerializeField]
    private ItemEjector _ejector;    

    public int InventorySize => _inventorySize;
    public List<BaseItemObject> StartItems => _startItems;
    public string SavePath => _savePath;
    public Transform Container => _container;
    public Transform DraggingParent => _draggingParent;    
    public ItemEjector Ejector => _ejector;
    public GameObject GameObject => gameObject;

    public void Awake()
    {        
        EnabledEvent?.Invoke();        
    }

    public void SwitchInventoryActivity(bool value)
    {
        SwitchedActivatorEvent?.Invoke(value);
    }

    public void AddItem(BaseItemObject item)
    {
        DraggedItemEvent?.Invoke(item, true);
    }

    private void OnDisable()
    {        
        DisabledEvent?.Invoke();
    }    
}
