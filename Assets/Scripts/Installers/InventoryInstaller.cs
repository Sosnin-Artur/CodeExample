using System;
using System.Reactive.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using MVP;
using UnityEngine.AddressableAssets;

public class InventoryInstaller : MonoInstaller
{
    [SerializeField] 
    private GameObject _inventory;    

    public override void InstallBindings()
    {
        var subContainer = Container.CreateSubContainer();
        var inventoryFactory = new PresenterFactory<IInventoryView, InventoryPresenter>(subContainer);        
                
        inventoryFactory.BindParamsAndCreate(_inventory, typeof(InventoryView), typeof(InventoryModel));
        inventoryFactory.UnbindPresenter();
    }    
}
