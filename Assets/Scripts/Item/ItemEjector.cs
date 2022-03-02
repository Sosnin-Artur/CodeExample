using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ItemEjector : MonoBehaviour
{        
    [SerializeField]
    private float _range;
    
    private GroundItemPool _pool;

    [Inject]
    public void Construct(GroundItemPool pool)
    {
        _pool = pool;
    }

    public void EjectFromPool(BaseItemObject item, Vector3 direction)
    {        
                      
        var presenter = _pool.Get();
        presenter.Item = item;
        
        var target = transform.position + (direction.normalized * _range);
        
        presenter.transform.position = target;
    }
}
