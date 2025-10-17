using ObjectPool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

public class ItemEjector : MonoBehaviour
{        
    [SerializeField]
    private float _range;        

    public void EjectFromPool(BaseItemObject item, Vector3 delta, 
        GenericObjectPool<BasePoolableGroundItem,
            IGroundItemFactory<BasePoolableGroundItem>> pool)
    {        
        var groundItem = pool.Get();
        
        if (groundItem)
        {
            groundItem.Item = item;

            var target = transform.position + delta.normalized * _range;

            groundItem.transform.position = target;
        }        
    }
}
