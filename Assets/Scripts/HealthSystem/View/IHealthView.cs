using System;
using MVP;
using UnityEngine.UI;

public interface IHealthView : IView
{               
    event Action<int> ApplyingDamageEvent;
    event Action<int> HealingEvent;

    int CurrentHealth { get; }
    int MaxHealth { get; }
    Image Healthbar { get; set; }

    void SetHealth(int value, int maxValue);
    void Heal(int value);
    void Die();
}