using UnityEngine;
using Zenject;

public class GroundItemFactory<T> : IGroundItemFactory<T>
    where T : BaseGroundItem
{    
    private readonly T _prefab;

    public GroundItemFactory(T prefab)
    {         
        _prefab = prefab;
    }

    public T Create()
    {        
        return GameObject.Instantiate(_prefab);
    }
}

