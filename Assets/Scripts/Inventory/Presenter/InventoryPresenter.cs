using ObjectPool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPresenter : BaseInventoryPresenter
{                
    private readonly InventoryCellPool _cellPool;
    private readonly GroundItemPool _groundItemPool;
    private readonly ItemObjectId _itemObjectIds;
    
    private IInventoryModel _model;   
    private List<BaseInventoryCell> _cells;    
    
    public List<BaseInventoryCell> Cells => _cells;

    public InventoryPresenter(
        IInventoryView view, 
        IInventoryModel model, 
        ItemObjectId itemObjectId,
        InventoryCellPool cellPool,
        GroundItemPool groundItemPool) : base(view)
    {
        _cellPool = cellPool;
        _groundItemPool = groundItemPool;
        _cells = new List<BaseInventoryCell>();
        _itemObjectIds = itemObjectId;

        InitModel(view, model);               

        ConnectView();        
    }    

    public void InitModel(IInventoryView view, IInventoryModel model)
    {           
        Load(view, model);
        
        _model.ItemToAdd = new ReactiveProperty<BaseGroundItem>(null);
        _model.ItemToEquip = new ReactiveProperty<BaseItemObject>(null);

        _model.ItemToAdd.Subscribe(x => AddItem(x));        
    }    

    public void Load(IInventoryView view, IInventoryModel model)
    {
        _model = model;
        model.Inventory = Saver.Load(View.SavePath) as InventoryData;        
                
        if (model.Inventory != null)
        {
            for (int i = 0, length = model.Inventory.Items.Length; i < length; i++)
            {
                if (model.Inventory.Items[i] != null)
                {
                    var obj = _itemObjectIds.ItemById[model.Inventory.Items[i].Id];
                    SetUpCell(obj);
                }
            }
        }
        else
        {            
            _model.Inventory = new InventoryData();
            _model.Inventory.Items = new ItemData[View.InventorySize];            

            foreach (var item in View.StartItems)
            {
                SetUpCell(item);
            }
        }
    }

    public void ConnectView()
    {        
        View.OnDraggedItemEvent += AddItem;
        View.OnDisabledEvent += OnDisable;
    }

    public override void Render(List<BaseInventoryCell> items)
    {
        for (int i = 0, length = items.Count; i < length; i++)
        {
            if (items[i] != null)
            {
                SetUpCell(items[i].Item);                
            }            
        }
    }

    public void AddItem(BaseItemObject item, bool withSave = true)
    {
        if (_cells.Count < View.InventorySize)
        {
            SetUpCell(item, withSave);
        }
    }

    public void AddItem(BaseGroundItem item, bool withSave = true)
    {        
        if (_cells.Count < View.InventorySize)
        {            
            SetUpCell(item.Item, withSave);

            _groundItemPool.Release(item as BasePoolableGroundItem);
        }
    }    

    private void SetUpCell(BaseItemObject item, bool withSave = false)
    {
        var cell = _cellPool.Get();
        _cells.Add(cell);

        cell.Init(View.DraggingParent, View.Container);
        cell.Render(item);

        cell.OnEjectingEvent += EjectItem;
        cell.OnEquipingEvent += EquipItem;
        
        if (withSave)
        {
            Save();
        }

    }

    private void Save()
    {                
        for (int i = 0, length = _model.Inventory.Items.Length; i < length; i++)
        {            
            if (i < _cells.Count)
            {                
                _model.Inventory.Items[i] = _cells[i].Item.ItemData;
            }
            else
            {
                _model.Inventory.Items[i] = null;
            }
        }
        
        Saver.Save(_model.Inventory, View.SavePath);
    }

    private void EquipItem(BaseItemObject item)
    {        
        _model.ItemToEquip.Value = item;
    }

    private void EjectItem(BaseInventoryCell cell, BaseItemObject item, Vector2 position)
    {        
        View.Ejector.EjectFromPool(item, position);
        _cellPool.Release(cell as BasePoolableInventoryCell);
        _cells.Remove(cell);
        cell.OnEjectingEvent -= EjectItem;
    }

    private void ReleaseItems()
    {
        for (int i = 0, length = _cells.Count; i < length; i++)
        {
            var cell = _cells[i];
            _model.Inventory.Items[i] = null;

            cell.OnEjectingEvent -= EjectItem;
            _cellPool.Release(cell as  BasePoolableInventoryCell);            
        }
        _cells.Clear();
    }

    private void OnDisable()
    {
        ReleaseItems();
    }    

    private void DisconnectView()
    {        
        View.OnDraggedItemEvent -= AddItem;
        View.OnDisabledEvent -= OnDisable;
    }

    public override void Dispose()
    {
        DisconnectView();

        OnDisable();
    }
}
