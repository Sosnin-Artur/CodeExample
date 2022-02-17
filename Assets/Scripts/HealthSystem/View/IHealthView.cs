using System;
using MVP;
public interface IHealthView : IView
{               
    int CurrentHealth { get; }
    int MaxHealth { get; }    
    void SetHealth(int value, int maxValue);
    void Die();
}