
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericObjectPool<T, TFactory> where TFactory : IFactory
{        
    protected readonly Queue<T> PooledObjects = new Queue<T>();    
    
    private IFactory<T> _factory;

    public GenericObjectPool(IFactory<T> factory, int count)
    {        
        Debug.Log("pool");
        _factory = factory;
        AddObjects(count);
    }

    public abstract T Get();        
    public abstract void Release(T objectToSet);        

    private void AddObjects(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var newObject = _factory.Create();                        
            Release(newObject);
        }
    }       
}