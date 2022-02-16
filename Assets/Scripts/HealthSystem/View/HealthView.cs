using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthView : MonoBehaviour, IHealthView
{
    [SerializeField]
    private int _currentHealth;
    [SerializeField]
    private int _maxHealth;    
    private BaseHealthPresenter _presenter;
        
    public BaseHealthPresenter Presenter 
    { 
        get
        {
            return _presenter;
        }
        set
        {            
            if (_presenter == null)
            {                
                _presenter = value;
            }
        }
    }
    
    public void Heal(int value)
    {
        _presenter.Heal(value);
    }

    public void TakeDamage(int value)
    {
        _presenter.TakeDamage(value);
    }

    private void Awake()
    {
        _presenter.InitModel(_currentHealth, _maxHealth);
        _presenter.OnDeathEvent += OnDeath;
    }

    private void OnDeath()
    {
        Debug.Log("Health: Death");
    }
}
