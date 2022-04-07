using UnityEngine;
using Zenject;

class HealthFactory : IHealthFactory
{    
    private readonly DiContainer _container;

    public HealthFactory(DiContainer container)
    {         
        _container = container;
    }

    public BaseHealthPresenter Create()
    {        
        return _container.Instantiate<HealthPresenter>();
    }
}

