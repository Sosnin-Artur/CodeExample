using UnityEngine;
using Zenject;

public class GroundItemFactory<T> : IGroundItemFactory<T>
    where T : BaseGroundItem
{    
    private readonly T _prefab;
    private readonly Transform _parent;

    public GroundItemFactory(T prefab, Transform parent)
    {         
        _prefab = prefab;
        _parent = parent;
    }

    public T Create()
    {        
        return GameObject.Instantiate(_prefab, _parent);
    }
}

