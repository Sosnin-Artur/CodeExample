using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] 
    private PlayerView _playerView;

    public override void InstallBindings()
    {
        Container.Bind<IPlayerView>().FromInstance(_playerView);
        Container.Bind<IModel>().To<PlayerModel>().AsTransient();
        Container.Bind<PlayerInputAction>().AsTransient();  
        
        Container.BindInterfacesAndSelfTo<PlayerPresenter>().AsTransient();
    }
}
