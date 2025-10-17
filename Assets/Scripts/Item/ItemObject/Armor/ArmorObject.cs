using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Armor Object", menuName = "Inventory System/Items/Armors")]
public class ArmorObject : BaseItemObject
{
    public void Awake()
    {
        ItemType = ItemType.Armor;
    }    
}
