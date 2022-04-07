using System;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class BaseEquipment
{
    public event Action StartingUsingCallbackEvent;
    public event Action EndingUsingCallbackEvent;

    protected BaseUsableItem UsableItem;        
    
    private int _armor = 1;    

    public int Armor => _armor;

    public void EquipItem(BaseItemObject item, Transform parent)
    {
        if (item.ItemType == ItemType.Weapon || item.ItemType == ItemType.Food)
        {
            GameObject.Destroy(UsableItem?.gameObject);
            UsableItem = null;
            UsableItem = GameObject.Instantiate(item.ItemRepresentation, parent) as BaseUsableItem;
            
            UsableItem.StartingUsingCallbackEvent += StartingUsingCallbackEvent;            
            UsableItem.EndingUsingCallbackEvent += EndingUsingCallbackEvent;
        }        
    }

    public abstract void Use();
    public abstract void RotateAxisX(float directionX);
}