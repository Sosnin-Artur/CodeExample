using UnityEngine;

public class EjectingEventArgs
{
    private BaseInventoryCell _inventoryCell;
    private BaseItemObject _itemObject;
    private Vector2 _position;

    public BaseInventoryCell InventoryCell => _inventoryCell;
    public BaseItemObject ItemObject => _itemObject;
    public Vector2 Position => _position;

    public EjectingEventArgs(
        BaseInventoryCell inventoryCell, 
        BaseItemObject itemObject, 
        Vector2 position)
    {
        _inventoryCell = inventoryCell;
        _itemObject = itemObject;
        _position = position;
    }
}