using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;
using Zenject;
using ObjectPool;
using MVP;

[TestFixture]
public class SpawnerTests : ZenjectUnitTestFixture
{   
    private BaseSpawnerPresenter _spawner;

    [SetUp]
    public void Init()
    {
        var view = new GameObject();
        view.AddComponent<SpawnerView>();
        var enemy = GameObject.Instantiate(Resources.Load(PathsInResources.Enemy)) as GameObject;
        
        var subContainer = Container.CreateSubContainer();
        var factory = new PresenterFactory<ISpawnerView, SpawnerPresenter>(subContainer);

        Container.BindInstance<int>(5);
        Container.BindInstance<GameObject>(enemy);
        Container.BindInstance<Transform>(view.GetComponent<Transform>());

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

        _spawner = factory.BindParamsAndCreate(
            view,
            typeof(SpawnerView),
            typeof(EnemyPool));        
    }

    [Test]
    public void WhenSpawnEnemy_AndPoolIsNotEmpty_ThenShouldAppeawrEnemy()
    {
        _spawner.CreateEnemy();

        var enemy = GameObject.Find("Enemy(Clone)");
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                
        Assert.IsTrue(enemy.activeInHierarchy);
    }
}
