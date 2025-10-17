using UnityEngine;
using Zenject;

public class ProjectileFactory<T> : IProjectileFactory<T> 
    where T : BaseProjectile
{    
    private readonly T _prefab;

    public ProjectileFactory(T prefab)
    {         
        _prefab = prefab;
    }

    public T Create()
    {        
        return GameObject.Instantiate(_prefab);
    }
}

