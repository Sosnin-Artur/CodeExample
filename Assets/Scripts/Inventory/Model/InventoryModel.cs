using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryModel : IInventoryModel
{                
    public InventoryData Inventory { get; set; }
    public ReactiveProperty<BaseGroundItem> ItemToAdd { get; set; }
    public ReactiveProperty<BaseItemObject> ItemToEquip { get; set; }    
}
