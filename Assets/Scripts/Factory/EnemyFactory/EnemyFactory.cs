using System.Collections.Generic;
using UnityEngine;
using Zenject;

class EnemyFactory : IEnemyFactory
{    
    private readonly DiContainer _container;    
    
    public EnemyFactory(DiContainer container)
    {        
        _container = container;                
    }

    public BaseEnemyPresenter Create()
    {                
        return _container.Instantiate<EnemyPresenter>();
    }
}

