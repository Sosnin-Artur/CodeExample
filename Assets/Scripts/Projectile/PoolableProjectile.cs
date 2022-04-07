using ObjectPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolableProjectile : BasePoolableProjectile
{    
    public override void Spawn()
    {
        gameObject.SetActive(true);        
    }

    public override void Release()
    {
        Rigidbody.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }
}
