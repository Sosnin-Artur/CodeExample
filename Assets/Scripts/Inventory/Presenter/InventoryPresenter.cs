using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPresenter : BaseInventoryPresenter
{            
    private readonly IInventoryModel _model;
    private readonly InventoryCellPool _pool;
    
    private List<BasePoolableInventoryCell> _cells;

    public InventoryPresenter(IInventoryView view, IInventoryModel model, InventoryCellPool pool) : base(view)
    {
        _model = model;
        _model.ItemToAdd = new ReactiveProperty<BaseGroundItem>(null);
        _model.ItemToAdd.Subscribe(x => AddItem(x));
        
        InitViewEvents();

        _pool = pool;

        model.Items = new BaseItemObject[view.InventorySize];        

        _cells = new List<BasePoolableInventoryCell>();
    }

    private void AddItem(BaseGroundItem item)
    {        
        if (_cells.Count < View.InventorySize)
        {            
            _model.Items.SetValue(item.Item, _cells.Count);
            _cells.Add(_pool.Get());
            Render(_model.Items);
            
            GameObject.Destroy(item.gameObject);
        }             
    }

    public void OnEnable()
    {
        Render(_model.Items);
    }

    public override void Render(BaseItemObject[] items)
    {        
        for (int i = 0, length = _cells.Count; i < length; i++)
        {                        
            var cell = _cells[i];
            var item = items[i];

            cell.Init(View.DraggingParent, View.Container);            
            
            cell.Render(item);

            cell.EjectingEvent += () => View.Ejector.EjectFromPool(item, View.Ejector.transform.right);
            cell.EjectingEvent += () => _pool.Release(cell);            
        }        
    }

    public void OnDisable()
    {
        ReleaseItems();
    }

    public override void Dispose()
    {
        View.OnEnabledEvent -= OnEnable;
        View.OnDisabledEvent -= OnDisable;
    }

    private void InitViewEvents()
    {
        View.OnEnabledEvent += OnEnable;
        View.OnDisabledEvent += OnDisable;
    }    

    private void ReleaseItems()
    {
        for (int i = 0, length = _cells.Count; i < length; i++)
        {            
            var cell = _cells[i];
            _model.Items[i] = null;            
        }
        _cells.Clear();
    }
}
