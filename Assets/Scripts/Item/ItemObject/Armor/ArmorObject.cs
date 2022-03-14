using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Armor Object", menuName = "Inventory System/Items/Armors")]
public class ArmorObject : BaseItemObject
{
    public void Awake()
    {
        ItemData.ItemType = ItemType.Armor;
    }

    public override BaseItem Equip(Transform parent)
    {        
        return null;
    }    
}
