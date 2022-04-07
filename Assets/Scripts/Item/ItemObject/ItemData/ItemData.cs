using System;
using UnityEngine;

[Serializable]
public class ItemData : IItemData
{
    [SerializeField]
    private int _id;    

    public int Id
    {
        get => _id;
        set => _id = value;
    }        
}