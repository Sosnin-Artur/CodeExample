using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;
using Zenject;

[TestFixture]
public class HealthSystemTests : ZenjectUnitTestFixture
{        
    private HealthPresenter _health;

    [SetUp]
    public void Init()
    {        
        var healthView = Substitute.For<IHealthView>();
        healthView.CurrentHealth.Returns(50);
        healthView.MaxHealth.Returns(100);

        Container.BindInstance(healthView);
        Container
            .Bind<IHealthModel>()
            .To<HealthModel>()
            .AsCached();
        Container
            .BindInterfacesAndSelfTo<HealthPresenter>()
            .AsCached();
        
        _health = Container.Resolve<HealthPresenter>();
    }   
    
    [Test]
    public void WhenApplyDamage_AndCurrentHealthEnough_ThenCurrentHealthShouldBeLess()
    {
        var health = _health.Model.CurrentHealth.Value;
        
        _health.ApplyDamage(5);

        Assert.Less(_health.Model.CurrentHealth.Value, health);
    }

    [Test]
    public void WhenApplyDamage_AndCurrentHealthNotEnough_ThenCurrentHealthShouldBeLess()
    {
        _health.Model.CurrentHealth.Value = 3;

        _health.ApplyDamage(5);

        Assert.AreEqual(_health.Model.CurrentHealth.Value, 0);
    }

    [Test]
    public void WhenHeal_AndMaxHealthEnough_ThenCurrentHealthShouldBeGreater()
    {
        var health = _health.Model.CurrentHealth.Value;

        _health.Heal(5);

        Assert.Greater(_health.Model.CurrentHealth.Value, health);
    }    

    [Test]
    public void WhenHeal_AndMaxHealthNotEnough_ThenCurrentHealthShouldBeGreater()
    {
        _health.Model.CurrentHealth.Value = _health.Model.MaxHealth.Value;

        _health.Heal(5);

        Assert.AreEqual(_health.Model.CurrentHealth.Value, _health.Model.MaxHealth.Value);
    }
}
