using System;
using System.Reactive.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using MVP;

public class PlayerInstaller : MvpInstaller
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
        CreateMvp<HealthModel, IHealthView, HealthPresenter>(_healthView);
    }

    private void InstallBindingPlayer()
    {
        Container.Bind<PlayerInputAction>().AsSingle();
        CreateMvp<PlayerModel, IPlayerView, PlayerPresenter>(_playerView);                                
    }
}
