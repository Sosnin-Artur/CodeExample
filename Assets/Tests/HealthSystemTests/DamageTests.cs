using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;
using Zenject;

[TestFixture]
public class DamageTests : ZenjectUnitTestFixture
{        
    private HealthPresenter _health;
    private GameObject _gameObject;
    [SetUp]
    public void Init()
    {                
        _gameObject = GameObject.Instantiate(Resources.Load(PathsInResources.Player)) as GameObject;
        var healthView = _gameObject.GetComponent<HealthView>();                

        Container.BindInstance<IHealthView>(healthView);
        Container
            .Bind<IHealthModel>()
            .To<HealthModel>()
            .AsCached();
        Container
            .BindInterfacesAndSelfTo<HealthPresenter>()
            .AsCached();
        
        _health = Container.Resolve<HealthPresenter>();
    }   
    
    [UnityTest]
    public IEnumerator WhenTouchDamager_AndNotDead_ThenCurrentHealthShouldBeLess()
    {
        var health = _health.Model.CurrentHealth.Value;        
        var damager = GameObject.Instantiate(Resources.Load(PathsInResources.Enemy)) as GameObject;
        damager.transform.position = _gameObject.transform.position;
        
        yield return new WaitForSeconds(0.1f);

        Assert.Less(_health.Model.CurrentHealth.Value, health);
    }    
}
