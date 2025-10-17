using System;
using System.Reactive.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using MVP;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Cinemachine;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField]    
    private Transform _position;

    [SerializeField]
    private AssetReference _playerReference;
    
    [SerializeField]
    private CinemachineVirtualCamera _virtualCamera;
    
    PresenterFactory<IPlayerView, PlayerPresenter> _playerFactory;
    PresenterFactory<IHealthView, HealthPresenter> _healthFactory;
    PresenterFactory<IInventoryView, InventoryPresenter> _inventoryFactory;

    public override void InstallBindings()
    {        
        var subContainer = Container.CreateSubContainer();

        _playerFactory = new PresenterFactory<IPlayerView, PlayerPresenter>(subContainer);
        _healthFactory = new PresenterFactory<IHealthView, HealthPresenter>(subContainer);
        _inventoryFactory = new PresenterFactory<IInventoryView, InventoryPresenter>(subContainer);

        var player = _playerReference.LoadAssetAsync<GameObject>();
        player.Completed += PlayerCompleted;        
    }

    private void PlayerCompleted(AsyncOperationHandle<GameObject> operation)
    {
        if (operation.Status == AsyncOperationStatus.Succeeded)
        {
            var obj = GameObject.Instantiate(
                operation.Result, 
                _position.position,
                Quaternion.identity, 
                transform.parent);

            _virtualCamera.Follow = obj.transform;

            _inventoryFactory.BindParamsAndCreate(
                obj.GetComponentInChildren<InventoryView>().gameObject,
                typeof(InventoryView),
                typeof(InventoryModel));

            _healthFactory.BindParamsAndCreate(
                obj,
                typeof(HealthView),
                typeof(HealthModel));

            _playerFactory.BindParamsAndCreate(
                obj,
                typeof(PlayerView),
                typeof(PlayerModel),
                typeof(InventoryModel),
                typeof(Equipment),
                typeof(PlayerInputAction));            

            _healthFactory.UnbindPresenter();
            _playerFactory.UnbindPresenter();
            _inventoryFactory.UnbindPresenter();
        }
    }    
}
