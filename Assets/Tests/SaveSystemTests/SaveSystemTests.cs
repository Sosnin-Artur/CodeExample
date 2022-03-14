using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;
using Zenject;
using ObjectPool;

[TestFixture]
public class SaveSystemTests : ZenjectUnitTestFixture
{   
    private string _savePath = "test";
    private ItemData _itemData;

    [SetUp]
    public void Init()
    {
        _itemData = new ItemData()
        { 
            Id = 0,
            Name = "test",
            ItemType = ItemType.Default
        };

        Saver.Save(_itemData, _savePath);
    }

    [Test]
    public void WhenLoad_ThenShouldBeTheSameData()
    {
        ItemData data = Saver.Load(_savePath) as ItemData;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                
        Assert.AreEqual(data.Id, _itemData.Id);
        Assert.AreEqual(data.Name, _itemData.Name);
        Assert.AreEqual(data.ItemType, _itemData.ItemType);
    }
}
