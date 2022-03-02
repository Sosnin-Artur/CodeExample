using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseItemObject : ScriptableObject
{
    [SerializeField]
    private int _id;
    [SerializeField]
    private string _name;
    [SerializeField]
    private Sprite _uiDisplay;
    [SerializeField]
    private ItemType _itemType;    

    public int Id 
    {
        get => _id;
        set => _id = value;
    }
    public Sprite UiDisplay => _uiDisplay;
    public string Name => _name;
    public ItemType ItemType
    { 
        get => _itemType;        
        set => _itemType = value;        
    }   
}
