using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryModel : IInventoryModel
{        
    private BaseItemObject[] _items;
    
    public BaseItemObject[] Items 
    { 
        get => _items; 
        set => _items = value; 
    }
    public ReactiveProperty<BaseGroundItem> ItemToAdd { get; set; }
}
