using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using NSubstitute;
using Zenject;
using System;
using ObjectPool;
using MVP;

[TestFixture]
public class ChestTests : ZenjectUnitTestFixture
{
    private InventoryPresenter _inventory;

    [SetUp]
    public void Init()
    {
        var inventoryView =
               GameObject.Instantiate(
                   Resources.Load(PathsInResources.Chest)) as GameObject;

        Container.BindInstance<Transform>((GameObject.Instantiate(
                Resources.Load(PathsInResources.PoolableObjectsParents)) as GameObject).GetComponent<Transform>());

        Container.BindInstance(5);
        var itemObjectId = ScriptableObject.CreateInstance<ItemObjectId>();
        itemObjectId.Awake();

        Container
            .BindInstance<ItemObjectId>(itemObjectId)
            .AsCached();
                
        BindInventory(inventoryView, itemObjectId);
    }

    private void BindInventory(GameObject gameObject, ItemObjectId itemObjectId)
    {        
        BindGroundPool();
        BindInventoryCellPool();        

        Container.BindInstance<IInventoryView>(gameObject.GetComponent<InventoryView>());

        var model = new InventoryModel();
        model.Inventory = null;

        var inventoryView = gameObject.GetComponentInChildren<InventoryView>();                

        _inventory = new InventoryPresenter(
            inventoryView,
            model,
            itemObjectId,
            Container.Resolve<InventoryCellPool>(),
            Container.Resolve<GroundItemPool>());        
    }

    private void BindInventoryCellPool()
    {        
        {            
            Container
                .Bind<IInventoryCellFactory<BasePoolableInventoryCell>>()
                .To<InventoryCellFactory<BasePoolableInventoryCell>>()
                .AsSingle();

            Container                
                .Bind<InventoryCellPool>()
                .AsSingle();

            var inventoryCell =
               (GameObject.Instantiate(
                   Resources.Load(PathsInResources.InventoryCell)) as GameObject)
                   .GetComponent<PoolableInventoryCell>();

            Container
                .BindInstance<BasePoolableInventoryCell>(inventoryCell)
                .WhenInjectedInto<InventoryCellFactory<BasePoolableInventoryCell>>();
        }
    }

    private void BindGroundPool()
    {        
        {            
            Container
                .Bind<IGroundItemFactory<BasePoolableGroundItem>>()
                .To<GroundItemFactory<BasePoolableGroundItem>>()
                .AsSingle();

            Container                
                .Bind<GroundItemPool>()
                .AsSingle();

            var groundItem =
               (GameObject.Instantiate(
                   Resources.Load(PathsInResources.GroundItem)) as GameObject)
                   .GetComponent<PoolableGroundItem>();

            Container
                .BindInstance<BasePoolableGroundItem>(groundItem)
                .WhenInjectedInto<GroundItemFactory<BasePoolableGroundItem>>();
        }
    }

    [Test]
    public void WhenInstantiate_AndStartItemsIsNotEmpty_ThenItemCountShouldBeStartItemsSize()
    {
        Assert.AreEqual(_inventory.Cells.Count, _inventory.View.StartItems.Count);
    }
}
