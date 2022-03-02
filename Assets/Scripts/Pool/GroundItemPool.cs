using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using ObjectPool;

public class GroundItemPool : GenericObjectPool<BasePoolableGroundItem, IGroundItemFactory<BasePoolableGroundItem>>
{
    public GroundItemPool(IGroundItemFactory<BasePoolableGroundItem> factory, int count) : base(factory, count)
    {
        
    }

    public override BasePoolableGroundItem Get()
    {
        var newObject = PooledObjects.Dequeue();
        newObject.Spawn();
        
        return newObject;
    }

    public override void Release(BasePoolableGroundItem objectToSet)
    {
        objectToSet.Release();        
        PooledObjects.Enqueue(objectToSet);
    }
}
