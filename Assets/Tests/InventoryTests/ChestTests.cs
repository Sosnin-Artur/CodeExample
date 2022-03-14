using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using NSubstitute;
using Zenject;
using System;

[TestFixture]
public class ChestTests : ZenjectUnitTestFixture
{        
    private InventoryPresenter _inventory;

    [SetUp]
    public void Init()
    {                
        var gameObject = GameObject.Instantiate(Resources.Load(PathsInResources.Chest)) as GameObject;
        var inventoryView = gameObject.GetComponentInChildren<InventoryView>();

        BindInventory(inventoryView.GetComponent<InventoryView>());
    }

    private void BindInventory(IInventoryView inventoryView)
    {
        Container.BindInstance(5);
        
        BindGroundPool();
        BindInventoryCellPool();

        var itemObjectId = ScriptableObject.CreateInstance<ItemObjectId>();
        
        Container.BindInstance<ItemObjectId>(itemObjectId).AsCached();        
        
        Container.BindInstance(inventoryView);
        Container
            .Bind<IInventoryModel>()
            .To<InventoryModel>()
            .AsCached();

        Container
            .BindInterfacesAndSelfTo<InventoryPresenter>()
            .AsCached();
        
        _inventory = Container.Resolve<InventoryPresenter>();        
    }

    private void BindGroundPool()
    {        
        var groundItemObject = GameObject.Instantiate(Resources.Load(PathsInResources.GroundItem)) as GameObject;
        var groundItem = groundItemObject.GetComponent<BasePoolableGroundItem>();        
        
        Container
            .Bind<GroundItemPool>()
            .AsCached();
        Container
            .Bind<IGroundItemFactory<BasePoolableGroundItem>>()
            .To<GroundItemFactory<BasePoolableGroundItem>>()
            .AsCached();  
        Container
            .BindInstance<BasePoolableGroundItem>(groundItem)
            .WhenInjectedInto<GroundItemFactory<BasePoolableGroundItem>>();
    }

    private void BindInventoryCellPool()
    {
        var inventoryCellObject = GameInstaller.Instantiate(Resources.Load(PathsInResources.InventoryCell)) as GameObject;
        var inventoryCell = inventoryCellObject.GetComponent<BasePoolableInventoryCell>();

        Container
            .Bind<InventoryCellPool>()
            .AsCached();
        Container
            .Bind<IInventoryCellFactory<BasePoolableInventoryCell>>()
            .To<InventoryCellFactory<BasePoolableInventoryCell>>()
            .AsCached();
        Container
            .BindInstance<BasePoolableInventoryCell>(inventoryCell)
            .WhenInjectedInto<InventoryCellFactory<BasePoolableInventoryCell>>();
    }

    [Test]
    public void WhenInstantiate_AndStartItemsIsNotEmpty_ThenItemCountShouldBeStartItemsSize()
    {                                                
        Assert.AreEqual(_inventory.Cells.Count, _inventory.View.StartItems.Count);
    }    
}
