using ObjectPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePoolableProjectile : BaseProjectile, IPoolable
{    
    public abstract void Spawn();    

    public abstract void Release();           
}
