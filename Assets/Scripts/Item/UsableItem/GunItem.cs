using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using Zenject;
using UnityEngine.InputSystem;

public class GunItem : BaseUsableItem
{    
    [SerializeField]
    private float _duration;    
    [SerializeField]
    private Transform _startPoint;

    private Transform _axis;    

    [Inject]
    private ProjectileFactory<BasePoolableProjectile> _factory;

    public void Awake()
    {
        _axis = transform.parent.parent;        
    }            

    public override void Use()
    {        
        
    }

    public override void Rotate(Quaternion angle)
    {        
        _axis.rotation = angle;
    }
}

