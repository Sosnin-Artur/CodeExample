using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;

public class HealthTests
{
    IHealthModel _model;
    IHealthView _view;
    BaseHealthPresenter _presenter;

    [SetUp]
    public void SetUp()
    {
        _view = Substitute.For<IHealthView>();       
        _model = new HealthModel();
        _presenter = new HealthPresenter(_view, _model);

        _presenter.InitModel(50, 100);
    }

    [Test]
    public void WhenInitModel_AndSetCurrentValueTo60_ThenCurrentHealthShouldBe60()
    {
        _presenter.InitModel(60, 120);

        Assert.AreEqual(60, ((HealthModel)_model).CurrentHealth);
    }

    [Test]
    public void WhenTakeDamage_AndValueIsLessThanCurrentHealth_ThenCurrentHealthShouldBeMoreThan0()
    {
        int value  = ((HealthModel)_model).CurrentHealth / 2;
        _presenter.TakeDamage(value);

        Assert.Greater(((HealthModel)_model).CurrentHealth, 0);
    }

    [Test]
    public void WhenTakeDamage_AndValueIsMoreThanCurrentHealth_ThenCurrentHealthShouldBe0()
    {
        int value = ((HealthModel)_model).CurrentHealth * 2;
        _presenter.TakeDamage(value);

        Assert.AreEqual(((HealthModel)_model).CurrentHealth, 0);
    }

    [Test]
    public void WhenHeal_AndValueIsSmall_ThenCurrentHealthShouldBeLessThanMaxHealth()
    {
        int value = 0;
        _presenter.Heal(value);

        Assert.Less(((HealthModel)_model).CurrentHealth, ((HealthModel)_model).MaxHealth);
    }

    [Test]
    public void WhenHeal_AndValueIsMoreThanMaxHealth_ThenCurrentHealthShouldBeMaxHealth()
    {
        int value = ((HealthModel)_model).MaxHealth * 2;
        _presenter.Heal(value);

        Assert.AreEqual(((HealthModel)_model).CurrentHealth, ((HealthModel)_model).MaxHealth);
    }
}

