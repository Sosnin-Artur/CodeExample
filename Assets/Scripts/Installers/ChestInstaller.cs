using MVP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

public class ChestInstaller : MonoInstaller
{
    [SerializeField]
    private Transform _position;

    [SerializeField]
    private AssetReference _chestReference;
    
    PresenterFactory<IInventoryView, InventoryPresenter> _inventoryFactory;

    public override void InstallBindings()
    {        
        var subContainer = Container.CreateSubContainer();
        
        _inventoryFactory = new PresenterFactory<IInventoryView, InventoryPresenter>(subContainer);

        var chest = _chestReference.LoadAssetAsync<GameObject>();
        chest.Completed += ChestCompleted;        
    }

    private void ChestCompleted(AsyncOperationHandle<GameObject> operation)
    {
        if (operation.Status == AsyncOperationStatus.Succeeded)
        {
            var obj = GameObject.Instantiate(
                operation.Result,
                _position.position,
                Quaternion.identity,
                transform.parent);

            _inventoryFactory.BindParamsAndCreate(
                obj.GetComponentInChildren<InventoryView>().gameObject,
                typeof(InventoryView),
                typeof(InventoryModel));            
            _inventoryFactory.UnbindPresenter();
        }
    }
}
