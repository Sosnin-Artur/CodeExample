using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using MVP;

public class SpawnerInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject _view;

    public override void InstallBindings()
    {        
        var subContainer = Container.CreateSubContainer();
        var factory = new PresenterFactory<ISpawnerView, SpawnerPresenter>(subContainer);
        
        factory.BindParamsAndCreate(
            _view, 
            typeof(SpawnerView), 
            typeof(EnemyPool));
    }    
}