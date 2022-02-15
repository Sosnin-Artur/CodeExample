using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] 
    private PlayerView _playerView;
    [SerializeField]
    private HealthView _healthView;

    public override void InstallBindings()
    {
        InstallBindingPlayerHealth();
        InstallBindingPlayer();        
    }

    private void InstallBindingPlayerHealth()
    {
        Container.Bind<IHealthView>().FromInstance(_healthView);
        Container.Bind<IHealthModel>().To<HealthModel>().AsTransient();
        Container.BindInterfacesAndSelfTo<HealthPresenter>().AsTransient();
    }

    private void InstallBindingPlayer()
    {
        Container.Bind<IPlayerView>().FromInstance(_playerView);
        Container.Bind<IPlayerModel>().To<PlayerModel>().AsTransient();
        Container.Bind<PlayerInputAction>().AsTransient();

        Container.BindInterfacesAndSelfTo<PlayerPresenter>().AsTransient();
    }
}
