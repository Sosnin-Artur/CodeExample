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

    public int CurrentHealth
    {
        get
        {
            return _currentHealth;
        }
    }

    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
    }

    public void SetHealth(int value, int maxValue)
    {
        // TODO : add healthbar.
    }

    public void Die()
    {
        Debug.Log("Health: Death");        
    }

    private void Awake()
    {                
        SetHealth(CurrentHealth, MaxHealth);
    }    
}
