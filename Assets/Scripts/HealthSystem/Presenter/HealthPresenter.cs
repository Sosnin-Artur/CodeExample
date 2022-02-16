using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class HealthPresenter : BaseHealthPresenter, IInitializable
{    
    private HealthModel _healthModel;    

    public HealthPresenter(IHealthView view, IHealthModel model) : base(view)
    {        
        _healthModel = model as HealthModel;        
        Initialize();
    }

    public void Initialize()
    {
        View.Presenter = this;
    }

    public override void InitModel(int currentHealth, int maxHealth)
    {
        _healthModel.CurrentHealth = currentHealth;
        _healthModel.MaxHealth = maxHealth;
    }

    public override void Heal(int value)
    {
        _healthModel.CurrentHealth += value;

        if (_healthModel.CurrentHealth > _healthModel.MaxHealth)
        {
            _healthModel.CurrentHealth = _healthModel.MaxHealth;
        }
    }            

    public override void TakeDamage(int value)
    {
        _healthModel.CurrentHealth -= value;

        if (_healthModel.CurrentHealth < 0)
        {
            _healthModel.CurrentHealth = 0;
            InvokeDeathEvent();
        }
    }    
}
