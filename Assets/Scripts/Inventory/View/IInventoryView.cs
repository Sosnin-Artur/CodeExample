using MVP;
using System;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryView : IView
{
    event Action EnabledEvent;    
    event Action DisabledEvent;
    event Action<bool> SwitchedActivatorEvent;
    event Action<BaseItemObject, bool> DraggedItemEvent;

    int InventorySize { get; }    
    List<BaseItemObject> StartItems { get; }
    string SavePath { get; }
    Transform DraggingParent { get; }
    Transform Container { get; }
    ItemEjector Ejector { get; }
    GameObject GameObject { get; }

    void AddItem(BaseItemObject item);
}