using System;

[Serializable]
public class InventoryData : IInventoryData
{
    private IItemData[] _items;

    public IItemData[] Items
    {
        get => _items;
        set => _items = value;
    }
}