using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using ObjectPool;

public class InventoryCellPool : 
    GenericObjectPool<BasePoolableInventoryCell, 
        IInventoryCellFactory<BasePoolableInventoryCell>>
{
    public InventoryCellPool(
        IInventoryCellFactory<BasePoolableInventoryCell> factory, 
        int count) : base(factory, count)
    {
        
    }
}
