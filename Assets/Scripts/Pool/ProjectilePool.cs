using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectPool;

public class Pool<T, F> : GenericObjectPool<T, F>
    where T : IPoolable
    where F : IFactory<T>
{
    public Pool(F factory, int count) : base(factory, count)
    {
        
    }    
}
