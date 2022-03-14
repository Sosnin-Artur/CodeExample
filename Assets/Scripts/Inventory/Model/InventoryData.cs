using System;

[Serializable]
public class InventoryData : IData
{
    private ItemData[] _items;

    public ItemData[] Items
    {
        get => _items;
        set => _items = value;
    }
}