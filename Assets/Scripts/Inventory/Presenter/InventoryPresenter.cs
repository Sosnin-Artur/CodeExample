using ObjectPool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPresenter : BaseInventoryPresenter
{                
    private readonly InventoryCellPool _cellPool;
    private readonly GroundItemPool _groundItemPool;

    private IInventoryModel _model;
    private List<BaseInventoryCell> _cells;

    public InventoryPresenter(
        IInventoryView view, 
        IInventoryModel model, 
        InventoryCellPool cellPool,
        GroundItemPool groundItemPool) : base(view)
    {
        InitModel(view, model);
        
        _cells = new List<BaseInventoryCell>();

        InitViewEvents();

        _cellPool = cellPool;
        _groundItemPool = groundItemPool;
        _cells = new List<BaseInventoryCell>();
    }

    public void OnEnable()
    {
        Render(_model.Items);
    }

    public void InitModel(IInventoryView view, IInventoryModel model)
    {
        _model = model;
        _model.ItemToAdd = new ReactiveProperty<BaseGroundItem>(null);
        _model.ItemToAdd.Subscribe(x => AddItem(x));
        _model.Items = new BaseItemObject[view.InventorySize];
    }

    public void InitViewEvents()
    {
        View.OnEnabledEvent += OnEnable;
        View.OnDisabledEvent += OnDisable;
    }

    public override void Render(BaseItemObject[] items)
    {
        for (int i = 0, length = items.Length; i < length; i++)
        {
            if (items[i] != null)
            {
                SetUpCell(items[i]);
            }            
        }
    }

    private void AddItem(BaseGroundItem item)
    {        
        if (_cells.Count < View.InventorySize)
        {
            _model.Items.SetValue(item.Item, _cells.Count);            

            SetUpCell(item.Item);

            _groundItemPool.Release(item as BasePoolableGroundItem);
        }
    }

    private void SetUpCell(BaseItemObject item)
    {
        var cell = _cellPool.Get();
        _cells.Add(cell);

        cell.Init(View.DraggingParent, View.Container);
        cell.Render(item);

        cell.EjectingEvent += EjectItem;
    }

    private void EjectItem(BaseInventoryCell cell, BaseItemObject item, Vector2 position)
    {        
        View.Ejector.EjectFromPool(item, position);
        _cellPool.Release(cell as BasePoolableInventoryCell);
        _cells.Remove(cell);
        cell.EjectingEvent -= EjectItem;
    }

    private void ReleaseItems()
    {
        for (int i = 0, length = _cells.Count; i < length; i++)
        {
            var cell = _cells[i];
            _model.Items[i] = null;

            cell.EjectingEvent -= EjectItem;
            _cellPool.Release(cell as  BasePoolableInventoryCell);            
        }
        _cells.Clear();
    }

    public void OnDisable()
    {
        ReleaseItems();
    }

    public override void Dispose()
    {
        View.OnEnabledEvent -= OnEnable;
        View.OnDisabledEvent -= OnDisable;

        OnDisable();
    }    
}
