using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Food Object", menuName = "Inventory System/Items/Food")]
public class FoodObject : BaseItemObject
{
    public void Awake()
    {
        ItemData.ItemType = ItemType.Food;
    }

    public override BaseItem Equip(Transform parent)
    {        
        return null;
    }    
}
