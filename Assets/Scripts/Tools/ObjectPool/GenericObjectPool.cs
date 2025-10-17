
using System.Collections.Generic;
using UnityEngine;
using ObjectPool;

namespace ObjectPool
{
    public abstract class GenericObjectPool<T, TFactory>
        where T : IPoolable
        where TFactory : IFactory
    {
        protected readonly Queue<T> PooledObjects = new Queue<T>();

        private readonly IFactory<T> _factory;

        public int Length => PooledObjects.Count;
        
        public GenericObjectPool(IFactory<T> factory, int count)
        {            
            _factory = factory;
            AddObjects(count);
        }

        public void AddObjects(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var newObject = _factory.Create();                                
                Release(newObject);
            }
        }
        
        public virtual T Get()
        {
            T newObject = default;

            if (Length > 0)
            {
                newObject = PooledObjects.Dequeue();
            }
            else
            {
                return default;
            }

            newObject.Spawn();

            return newObject;
        }

        public virtual void Release(T objectToSet)
        {
            objectToSet.Release();
            PooledObjects.Enqueue(objectToSet);
        }
    }
}