using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseItemObject : ScriptableObject
{        
    [SerializeField]
    private ItemData _itemData;
    [SerializeField]
    private Sprite _uiDisplay;

    public virtual ItemData ItemData => _itemData;
    public Sprite UiDisplay => _uiDisplay;
    public abstract BaseItem Equip(Transform parent);       
}
