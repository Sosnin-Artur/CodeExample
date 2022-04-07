using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryModel : IInventoryModel
{
    public ReactiveProperty<bool> OpenInventory { get; set; }
    public IInventoryData Inventory { get; set; }
    public ReactiveProperty<BaseGroundItem> ItemToAdd { get; set; }
    public ReactiveProperty<BaseItemObject> ItemToEquip { get; set; }    
}
