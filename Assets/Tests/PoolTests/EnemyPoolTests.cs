using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;
using Zenject;

[TestFixture]
public class EnemyPoolTests : ZenjectUnitTestFixture
{        
    private EnemyPool _pool;
    private int _length = 5;
    
    [SetUp]
    public void Init()
    {
        Container.BindInstance<int>(_length);           

        Container
            .Bind<IEnemyFactory>()
            .To<EnemyFactory>()
            .AsSingle();

        Container            
            .Bind<EnemyPool>()
            .AsSingle();

        Container
            .BindInstance<GameObject>(Resources.Load(PathsInResources.Enemy) as GameObject )
            .WhenInjectedInto<EnemyFactory>();

        _pool = Container.Resolve<EnemyPool>();
    }    

    [Test]
    public void WhenGettingItem_AndPoolIsNotEmpty_ThenShouldBeItem()
    {
        var item = _pool.Get();

        Assert.IsNotNull(item);
    }

    [Test]
    public void WhenGettingItem_AndPoolIsEmpty_ThenShouldBeNull()
    {
        for (int i = 0; i < _length; i++)
        {
            _pool.Get();
        }
        var item = _pool.Get();
        
        Assert.IsNull(item);
    }

    [Test]
    public void WhenReleaseItem_AndPoolWithoutOneItem_ThenPoolShouldBeFull()
    {
         
        _pool.Release(_pool.Get());
        
        int length = _pool.Length;

        Assert.AreEqual(length, _length);
    }
}
