using System;
using UnityEngine;

[Serializable]
public class ItemData : IData
{
    [SerializeField]
    private int _id;
    [SerializeField]
    private string _name;    
    [SerializeField]
    private ItemType _itemType;

    public int Id
    {
        get => _id;
        set => _id = value;
    }
    
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