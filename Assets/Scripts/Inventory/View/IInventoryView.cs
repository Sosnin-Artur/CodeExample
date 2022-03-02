using MVP;
using System;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryView : IView
{
    event Action OnEnabledEvent;
    event Action OnDisabledEvent;

    int InventorySize { get; }
    Transform DraggingParent { get; }
    Transform Container { get; }
    ItemEjector Ejector { get; }

}