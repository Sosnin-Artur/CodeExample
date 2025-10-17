using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Ids Object", menuName = "Inventory System/Items/Ids")]
public class ItemObjectId : ScriptableObject
{
    [Serializable]
    public class Element
    {
        [SerializeField]
        private int _id;
        [SerializeField]
        private BaseItemObject _item;

        public int Id => _id;
        public BaseItemObject Item => _item;
    }

    [SerializeField]
    private List<Element> _elements;         
    
    private Dictionary<int, BaseItemObject> _itemById;

    public Dictionary<int, BaseItemObject> ItemById => _itemById;

    public void Awake()
    {
        _itemById = new Dictionary<int, BaseItemObject>();

        if (_elements != null)
        { 
            foreach (var element in _elements)
            {
                if (!ItemById.ContainsKey(element.Id))
                {
                    ItemById.Add(element.Id, element.Item);
                }                     
            }
        }
    }
}
