using MVP;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using ObjectPool;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class GameInstaller : MonoInstaller
{   
    [SerializeField]
    private int _enemyInPool;        
    [SerializeField]
    private AssetReference _enemyAssetReference;
    
    [SerializeField]
    private int _inventoryCellInPool;    
    [SerializeField]
    private AssetReference _inventoryCellAssetReference;

    [SerializeField]
    private int _groundItemInPool;
    [SerializeField]    
    private AssetReference _groundItemAssetReference;

    [SerializeField]
    private ItemObjectId _itemObjectId;

    public override void InstallBindings()
    {                        
        Container
            .BindInstance<Transform>(transform)
            .AsCached();

        var enemyHandle = _enemyAssetReference.LoadAssetAsync<GameObject>();
        enemyHandle.Completed += BindingEnemy;
        
        var inventoryCellHandle = _inventoryCellAssetReference.LoadAssetAsync<GameObject>();
        inventoryCellHandle.Completed += BindingInventoryCell;

        var groundItemHandle = _groundItemAssetReference.LoadAssetAsync<GameObject>();
        groundItemHandle.Completed += BindingGroundItem;        

        Container
            .BindInstance<ItemObjectId>(_itemObjectId)
            .AsCached();
        _itemObjectId.Awake();                
    }

    private void BindingInventoryCell(AsyncOperationHandle<GameObject> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            Container
                .BindInstance<int>(_inventoryCellInPool)
                .WhenInjectedInto<InventoryCellPool>();

            Container
                .Bind<IInventoryCellFactory<BasePoolableInventoryCell>>()
                .To<InventoryCellFactory<BasePoolableInventoryCell>>()
                .AsSingle();

            Container
                .Bind<GenericObjectPool<BasePoolableInventoryCell,
                      IInventoryCellFactory<BasePoolableInventoryCell>>>()
                .To<InventoryCellPool>()
                .AsSingle();
            
            Container
                .BindInstance<BasePoolableInventoryCell>(obj.Result.GetComponent<BasePoolableInventoryCell>())
                .WhenInjectedInto<InventoryCellFactory<BasePoolableInventoryCell>>();
        }        
    }
    
    private void BindingGroundItem(AsyncOperationHandle<GameObject> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            Container
                .BindInstance<int>(_groundItemInPool)
                .WhenInjectedInto<GroundItemPool>();

            Container
                .Bind<IGroundItemFactory<BasePoolableGroundItem>>()
                .To<GroundItemFactory<BasePoolableGroundItem>>()
                .AsSingle();

            Container
                .Bind<GenericObjectPool<BasePoolableGroundItem,
                      IGroundItemFactory<BasePoolableGroundItem>>>()
                .To<GroundItemPool>()
                .AsSingle();

            Container
                .BindInstance<BasePoolableGroundItem>((obj.Result.GetComponent<BasePoolableGroundItem>()))
                .WhenInjectedInto<GroundItemFactory<BasePoolableGroundItem>>();
        }            
    }

    private void BindingEnemy(AsyncOperationHandle<GameObject> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            Container
                .BindInstance<int>(_enemyInPool)
                .WhenInjectedInto<EnemyPool>();

            Container
                .Bind<IEnemyFactory<BasePoolableEnemyPresenter>>()
                .To<EnemyFactory<BasePoolableEnemyPresenter, PoolableEnemyPresenter>>()
                .AsSingle();

            Container
                .Bind<GenericObjectPool<
                    BasePoolableEnemyPresenter,
                    IEnemyFactory<BasePoolableEnemyPresenter>>>()
                .To<EnemyPool>()
                .AsSingle();

            Container
                .BindInstance<GameObject>(_enemyAssetReference.Asset as GameObject)
                .WhenInjectedInto<EnemyFactory<BasePoolableEnemyPresenter, PoolableEnemyPresenter>>();
        }
    }
}
