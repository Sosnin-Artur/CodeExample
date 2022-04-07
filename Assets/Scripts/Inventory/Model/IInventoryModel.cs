using MVP;
using System.Collections.Generic;

public interface IInventoryModel : IModel
{
    ReactiveProperty<bool> OpenInventory { get; set; } 
    IInventoryData Inventory { get; set; }
    ReactiveProperty<BaseGroundItem> ItemToAdd { get; set; }
    ReactiveProperty<BaseItemObject> ItemToEquip { get; set; }
}