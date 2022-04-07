using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : BaseDamager
{
    [SerializeField]
    private int _damage = 1;
    [SerializeField]
    private float _knockbackForce;

    public override int Damage 
    {
        get => _damage;
        set => _damage = value;
    }

    public override float KnockbackForce
    {
        get => _knockbackForce;
        set => _knockbackForce = value;
    }
}
