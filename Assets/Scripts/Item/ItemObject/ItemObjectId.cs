using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Ids Object", menuName = "Inventory System/Items/Ids")]
public class ItemObjectId : ScriptableObject
{        
    [SerializeField]
    private List<Element> _elements;    
    
    [Serializable]
    public class Element
    { 
        public int Id;
        public BaseItemObject Item;
    }

    public Dictionary<int, BaseItemObject> ItemById;

    public void Awake()
    {
        ItemById = new Dictionary<int, BaseItemObject>();

        if (_elements != null)
        { 
            foreach (var element in _elements)
            {
                ItemById.Add(element.Id, element.Item);
            }
        }
    }
}
