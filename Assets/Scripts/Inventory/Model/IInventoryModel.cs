using MVP;
using System.Collections.Generic;

public interface IInventoryModel : IModel
{
    BaseItemObject[] Items { get; set; }
    ReactiveProperty<BaseGroundItem> ItemToAdd { get; set; }
}