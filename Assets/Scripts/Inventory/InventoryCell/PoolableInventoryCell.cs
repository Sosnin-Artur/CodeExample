using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System;
using ObjectPool;

public class PoolableInventoryCell : BasePoolableInventoryCell
{
    public override void Init(Transform draggingParent, Transform container)
    {
        transform.SetParent(container);
        Init(draggingParent);
    }        
}
