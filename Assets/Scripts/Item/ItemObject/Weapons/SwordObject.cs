using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sword Object", menuName = "Inventory System/Items/Weapons/Sword")]
public class SwordObject : WeaponObject
{        
    [SerializeField]
    private SwordItem _sword;

    private SwordItem _instance;

    public override BaseItem Equip(Transform parent)
    {        
        _instance = Instantiate(_sword, parent.position, Quaternion.identity, parent);
                
        _instance.SetUp(this);
        
        return _instance;
    }        
}
