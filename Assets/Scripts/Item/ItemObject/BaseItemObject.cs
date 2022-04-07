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
    [SerializeField]
    private BaseItem _itemRepresentation;
    [SerializeField]
    private string _name;
    [SerializeField]
    private ItemType _itemType;

    public virtual IItemData ItemData => _itemData;
    public Sprite UiDisplay => _uiDisplay;    
    public BaseItem ItemRepresentation => _itemRepresentation;
    public string Name
    {
        get => _name;
        set => _name = value;
    }

    public ItemType ItemType
    {
        get => _itemType;
        set => _itemType = value;
    }
}
