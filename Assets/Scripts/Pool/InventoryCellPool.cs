using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using ObjectPool;

public class InventoryCellPool : GenericObjectPool<BasePoolableInventoryCell, IInventoryCellFactory<BasePoolableInventoryCell>>
{
    public InventoryCellPool(IInventoryCellFactory<BasePoolableInventoryCell> factory, int count) : base(factory, count)
    {
        
    }

    public override BasePoolableInventoryCell Get()
    {
        var newObject = PooledObjects.Dequeue();
        newObject.Spawn();
        
        return newObject;
    }

    public override void Release(BasePoolableInventoryCell objectToSet)
    {
        objectToSet.Release();        
        PooledObjects.Enqueue(objectToSet);
    }
}
