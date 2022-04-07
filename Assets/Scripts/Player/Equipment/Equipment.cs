using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Equipment : BaseEquipment
{
    public override void RotateAxisX(float directionX)
    {
        if (directionX > 0)
        {
            UsableItem?.Rotate(Quaternion.identity);
        }        
        else
        {
            UsableItem?.Rotate(Quaternion.Euler(0, 0, 180));
        }
    }

    public override void Use()
    {                
        UsableItem?.Use();
    }
}
