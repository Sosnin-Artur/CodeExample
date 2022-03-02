using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Food Object", menuName = "Inventory System/Items/Food")]
public class FoodObject : BaseItemObject
{
    public void Awake()
    {
        ItemType = ItemType.Food;
    }
}
