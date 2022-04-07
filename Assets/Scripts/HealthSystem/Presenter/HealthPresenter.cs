using System;
using System.Reactive.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPresenter : BaseHealthPresenter
{    
    private IHealthModel _healthModel;        

    public override IHealthModel Model
    {
        get
        {
            return _healthModel;
        }
    }

    public HealthPresenter(IHealthView view, IHealthModel model) : base(view)
    {                
        View.ApplyingDamageEvent += ApplyDamage;
        View.HealingEvent += Heal;

        InitModel(model);

        View.SetHealth(_healthModel.CurrentHealth.Value, _healthModel.MaxHealth.Value);
    }
    
    public void InitModel(IHealthModel model)
    {
        _healthModel = model;

        _healthModel.CurrentHealth = new ReactiveProperty<int>(View.CurrentHealth);
        _healthModel.MaxHealth = new ReactiveProperty<int>(View.MaxHealth);

        _healthModel.CurrentHealth.Subscribe(x => View.SetHealth(x, _healthModel.MaxHealth.Value));
        _healthModel.MaxHealth.Subscribe(x => View.SetHealth(_healthModel.CurrentHealth.Value, x));
        _healthModel.CurrentHealth.Subscribe(x => CallDeath(x));
    }

    public override void Heal(int value)
    {        
        _healthModel.CurrentHealth.Value = 
            Mathf.Min(_healthModel.CurrentHealth.Value + value, _healthModel.MaxHealth.Value);        
    }            

    public override void ApplyDamage(int value)
    { 
        _healthModel.CurrentHealth.Value = 
            Mathf.Max(_healthModel.CurrentHealth.Value - value, 0); ;                
    }        

    public override void Dispose()
    {
        _healthModel.CurrentHealth.Dispose();
        _healthModel.MaxHealth.Dispose();
    }    

    private void CallDeath(int x)
    {        
        if (x <= 0)
        {
            View.Die();
        }
    }
}
