using UnityEngine;
using Zenject;

public class InventoryCellFactory<T> : IInventoryCellFactory<T> 
    where T : BaseInventoryCell
{    
    private readonly T _prefab;

    public InventoryCellFactory(T prefab)
    {         
        _prefab = prefab;
    }

    public T Create()
    {        
        return GameObject.Instantiate(_prefab);
    }
}

