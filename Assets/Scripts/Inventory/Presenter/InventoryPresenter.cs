using ObjectPool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPresenter : BaseInventoryPresenter
{                
    private readonly GenericObjectPool<
        BasePoolableInventoryCell,
        IInventoryCellFactory<BasePoolableInventoryCell>> _cellPool;
    private readonly GenericObjectPool<
        BasePoolableGroundItem,
        IGroundItemFactory<BasePoolableGroundItem>> _groundItemPool;
    private readonly ItemObjectId _itemObjectIds;
    
    private IInventoryModel _model;   
    private List<BaseInventoryCell> _cells;    
    
    public List<BaseInventoryCell> Cells => _cells;

    public InventoryPresenter(
        IInventoryView view, 
        IInventoryModel model, 
        ItemObjectId itemObjectId,
        GenericObjectPool<
            BasePoolableInventoryCell,
            IInventoryCellFactory<BasePoolableInventoryCell>> cellPool,
        GenericObjectPool<
            BasePoolableGroundItem,
            IGroundItemFactory<BasePoolableGroundItem>> groundItemPool) : base(view)
    {
        _cellPool = cellPool;
        _groundItemPool = groundItemPool;
        _cells = new List<BaseInventoryCell>();
        _itemObjectIds = itemObjectId;

        InitModel(model);        
        ConnectView();        
    }    

    public void InitModel(IInventoryModel model)
    {           
        Load(model);
        
        _model.ItemToAdd = new ReactiveProperty<BaseGroundItem>(null);
        _model.ItemToEquip = new ReactiveProperty<BaseItemObject>(null);
        _model.OpenInventory = new ReactiveProperty<bool>(false);

        _model.ItemToAdd.Subscribe(x => AddItem(x));
        _model.OpenInventory.Subscribe(x => OpenInventory(x));

        OpenInventory(false);
    }    

    public void Load(IInventoryModel model)
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
        View.SwitchedActivatorEvent += OpenInventory;
        View.DraggedItemEvent += AddItem;
        View.DisabledEvent += OnDisable;
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

    public void OpenInventory(bool x)
    {
        View.GameObject.SetActive(x);
        
        for (int i = 0, length = View.Container.childCount; 
                i < length; i++)
        {
            View.Container.GetChild(i).gameObject.SetActive(x);
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

        cell.BeginingDragEvent += RemoveItem;
        cell.EndingDragEvent += ReturnItemToPool;

        cell.EjectingEvent += EjectItem;
        cell.EquipingEvent += EquipItem;
        
        if (withSave)
        {
            Save();
        }
    }    

    private async void Save()
    {
        IInventoryData data = _model.Inventory;

        for (int i = 0, length = _model.Inventory.Items.Length; i < length; i++)
        {            
            if (i < _cells.Count)
            {                
                data.Items[i] = _cells[i].Item.ItemData;
            }
            else
            {
                data.Items[i] = null;
            }
        }
        
        await Saver.SaveAsync(data, View.SavePath);
    }

    private void EquipItem(BaseItemObject item)
    {        
        _model.ItemToEquip.Value = item;
    }

    private void RemoveItem(BaseInventoryCell item)
    {
        _cells.Remove(item);
        Save();
    }

    private void ReturnItemToPool(BaseInventoryCell item)
    {
        _cellPool.Release(item as BasePoolableInventoryCell);
    }

    private void EjectItem(EjectingEventArgs ejectingEventArgs)
    {        
        View.Ejector.EjectFromPool(ejectingEventArgs.ItemObject, 
            ejectingEventArgs.Position, 
            _groundItemPool);
        _cellPool.Release(ejectingEventArgs.InventoryCell as BasePoolableInventoryCell);
        _cells.Remove(ejectingEventArgs.InventoryCell);
        
        ejectingEventArgs.InventoryCell.EjectingEvent -= EjectItem;
        ejectingEventArgs.InventoryCell.EquipingEvent -= EquipItem;

        ejectingEventArgs.InventoryCell.BeginingDragEvent -= RemoveItem;
        ejectingEventArgs.InventoryCell.EndingDragEvent -= ReturnItemToPool;

        Save();
    }    

    private void ReleaseItems()
    {
        for (int i = 0, length = _cells.Count; i < length; i++)
        {
            var cell = _cells[i];
            _model.Inventory.Items[i] = null;

            cell.EjectingEvent -= EjectItem;
            cell.EquipingEvent -= EquipItem;

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
        View.SwitchedActivatorEvent -= OpenInventory;
        View.DraggedItemEvent -= AddItem;
        View.DisabledEvent -= OnDisable;
    }

    public override void Dispose()
    {
        DisconnectView();

        OnDisable();
    }
}
