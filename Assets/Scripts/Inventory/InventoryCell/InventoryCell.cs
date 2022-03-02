using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System;

public class InventoryCell : BaseInventoryCell
{            
    public override void Init(Transform draggingParent, Transform container)
    {
        transform.parent = container;
        Init(draggingParent);
    }        
}
