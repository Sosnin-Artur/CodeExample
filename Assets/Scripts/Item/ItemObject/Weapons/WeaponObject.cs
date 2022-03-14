using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Object", menuName = "Inventory System/Items/Weapons")]
public abstract class WeaponObject : BaseItemObject
{        
    public void Awake()
    {
        ItemData.ItemType = ItemType.Weapon;
    }    
}
