using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using MVP;

//public class EnemyInstaller : MvpInstaller
public class HealthInstaller : Installer<HealthInstaller>
{
    //[SerializeField]
    //private EnemyView _enemyView;
    //[SerializeField]
    //private HealthView _healthView;
    
    readonly IHealthView _healthView;

    public HealthInstaller(HealthView health)
    {         
        _healthView = health;
    }

    public override void InstallBindings()
    {
        //InstallBindingEnemyHealth();
        //InstallBindingEnemy();                       
                        
        Container.BindInstance(_healthView).AsSingle();
        Container.BindInterfacesAndSelfTo<HealthModel>().AsSingle();        
        Container.BindInterfacesAndSelfTo<HealthPresenter>().AsSingle();
    }

    private void InstallBindingEnemyHealth()
    {
        //CreateMvp<IHealthView, HealthPresenter>(_healthView, typeof(HealthModel));
    }

    private void InstallBindingEnemy()
    {        
        //CreateMvp<IEnemyView, EnemyPresenter>(_enemyView, typeof(EnemyModel)
            //, typeof(HealthModel), new EnemyStateMachine(_enemyView));
    }
}
