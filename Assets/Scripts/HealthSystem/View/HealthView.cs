using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour, IHealthView
{
    public event Action<int> ApplyingDamageEvent;
    public event Action<int> HealingEvent;

    [SerializeField]
    private int _currentHealth = 50;
    [SerializeField]
    private int _maxHealth = 100;
    [SerializeField]
    private Rigidbody2D _rigidbody;
    [SerializeField]
    private LayerMask _mask;
    [SerializeField]
    private Image _healthbar;

    public int CurrentHealth => _currentHealth;
    public int MaxHealth => _maxHealth;

    public Image Healthbar
    {
        get => _healthbar;
        set => _healthbar = value;
    }

    public void SetHealth(int value, int maxValue)
    {        
        Healthbar.fillAmount = (float)value / maxValue;
    }

    public void Heal(int value)
    {
        HealingEvent?.Invoke(value);
    }

    public void Die()
    {        
        SetHealth(0, 1);
    }    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsInLayerMask(_mask, collision.gameObject))
        {
            InvokeApplyDamage(collision.gameObject.GetComponent<BaseDamager>());
        }
    }    

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (IsInLayerMask(_mask, collider.gameObject))
        {                        
            InvokeApplyDamage(
                collider.gameObject.GetComponent<BaseDamager>());
        }
    }

    private void InvokeApplyDamage(BaseDamager baseDamager)
    {
        var direction = (transform.position - baseDamager.transform.position).normalized;
        
        _rigidbody.AddForce(direction * baseDamager.KnockbackForce);
        ApplyingDamageEvent?.Invoke(baseDamager.Damage);
    }

    private bool IsInLayerMask(LayerMask mask, GameObject obj)
    {
        return ((mask.value & (1 << obj.layer)) > 0);
    }    
}
