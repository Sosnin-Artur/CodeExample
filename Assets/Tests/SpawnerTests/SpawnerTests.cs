using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;
using Zenject;
using ObjectPool;

[TestFixture]
public class SpawnerTests : ZenjectUnitTestFixture
{   
    private BaseSpawnerPresenter _spawner;

    [SetUp]
    public void Init()
    {
        var spawnerObject = GameObject.Instantiate(Resources.Load(PathsInResources.Spawner)) as GameObject;                        
        ISpawnerView spawnerView = spawnerObject.GetComponent<SpawnerView>();

        Container.BindInstance(spawnerView).AsCached();

        Container
            .Bind<IEnemyFactory>()
            .To<EnemyFactory>()
            .AsSingle();
        
        Container
            .BindInstance<GameObject>(GameObject.Instantiate(Resources.Load(PathsInResources.Enemy)) as GameObject)
            .WhenInjectedInto<EnemyFactory>();

        Container
            .BindInstance(5)            
            .AsCached();

        Container
            .Bind<GenericObjectPool<BasePoolableEnemyPresenter, IEnemyFactory>>()
            .To<EnemyPool>()
            .AsCached();

        Container
            .BindInterfacesAndSelfTo<SpawnerPresenter>()
            .AsCached();

        _spawner = Container.Resolve<SpawnerPresenter>();
    }

    [Test]
    public void WhenSpawnEnemy_AndPoolIsNotEmpty_ThenShouldAppeawrEnemy()
    {
        _spawner.CreateEnemy();

        var enemy = GameObject.Find("Enemy(Clone)");
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                
        Assert.IsNotNull(enemy);
    }
}
