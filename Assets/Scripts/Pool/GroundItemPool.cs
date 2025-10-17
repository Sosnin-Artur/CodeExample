using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using ObjectPool;

public class GroundItemPool : 
    GenericObjectPool<BasePoolableGroundItem, 
        IGroundItemFactory<BasePoolableGroundItem>>
{
    public GroundItemPool(
        IGroundItemFactory<BasePoolableGroundItem> factory, 
        int count) : base(factory, count)
    {
        
    }   
}
