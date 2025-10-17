using UnityEngine;
using Zenject;

public class InventoryCellFactory<T> : IInventoryCellFactory<T> 
    where T : BaseInventoryCell
{    
    private readonly T _prefab;
    private readonly Transform _parent;
    public InventoryCellFactory(T prefab, Transform parent)
    {         
        _prefab = prefab;
        _parent = parent;
    }

    public T Create()
    {        
        return GameObject.Instantiate(_prefab, _parent);
    }
}

