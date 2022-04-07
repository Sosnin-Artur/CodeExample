using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using MVP;
using System;

public class EnemyInstaller : Installer<EnemyInstaller>
{    
    public override void InstallBindings()
    {
        Container.Bind<IEnemyView>().To<EnemyView>().AsSingle();
        Container.Bind<IEnemyModel>().To<EnemyModel>().AsSingle();
        Container.BindInterfacesAndSelfTo<HealthModel>().AsSingle().WhenInjectedInto<BasePoolableEnemyPresenter>();
        Container.Bind<BaseEnemyPresenter>().To<BasePoolableEnemyPresenter>().AsSingle();        
    }    
}
