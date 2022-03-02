using ObjectPool;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class BasePoolableGroundItem : BaseGroundItem, IPoolable
{
    public abstract void Spawn();
    
    public abstract void Release();    
}
