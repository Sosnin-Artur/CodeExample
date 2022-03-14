using MVP;
using System.Collections.Generic;

public interface IInventoryModel : IModel
{
    InventoryData Inventory { get; set; }
    ReactiveProperty<BaseGroundItem> ItemToAdd { get; set; }
    ReactiveProperty<BaseItemObject> ItemToEquip { get; set; }
}