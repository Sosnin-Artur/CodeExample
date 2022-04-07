using System;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public abstract class BaseInventoryCell : 
    MonoBehaviour, 
    IBeginDragHandler, 
    IDragHandler, 
    IEndDragHandler,
    IPointerClickHandler
{
    public event Action<BaseInventoryCell> BeginingDragEvent;
    public event Action<BaseInventoryCell> EndingDragEvent;
    
    public event Action<EjectingEventArgs> EjectingEvent;
    public event Action<BaseItemObject> EquipingEvent;
    public event Action<BaseItemObject, bool> InseringItemEvemt;

    [SerializeField]
    private Text _nameField;
    [SerializeField]
    private Image _icon;

    private Transform _draggingParent;
    private Transform _originalParent;

    private BaseItemObject _item;

    public BaseItemObject Item => _item;   

    public virtual void Init(Transform draggingParent, Transform parent)
    {
        transform.SetParent(parent);
        _draggingParent = draggingParent;
        _originalParent = transform.parent;
    }

    public virtual void Init(Transform draggingParent)
    {
        _draggingParent = draggingParent;
        _originalParent = transform.parent;
    }

    public virtual void Render(BaseItemObject item)
    {        
        _item = item;        
        _nameField.text = item.Name;
        _icon.sprite = item.UiDisplay;        
    }    

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        BeginingDragEvent?.Invoke(this);
        transform.SetParent(_draggingParent);
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        transform.position = Mouse.current.position.ReadValue();
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {        
        var hoveredCount = eventData.hovered.Count;        
        if (hoveredCount > 0)
        {           
            IInventoryView comp = null;

            for (int i = 0, length = hoveredCount; i < length; i++)
            {
                comp = eventData.hovered[i].GetComponent<InventoryView>();                
                if (comp != null)
                {
                    break;
                }
            }
            
            if (comp != null)
            {
                EndingDragEvent?.Invoke(this);
                comp.AddItem(_item);
                return;
            }            
        }                      
        
        ExtractItem(eventData);                
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {               
        var comp = eventData.hovered[eventData.hovered.Count - 1];
        
        if (comp != null)
        {            
            EquipingEvent?.Invoke(_item);            
        }
    }

    private void ExtractItem(PointerEventData eventData)
    {
        EjectingEvent?.Invoke(
            new EjectingEventArgs(
                this,
                _item,
                eventData.delta));
    }

    private bool In(RectTransform originalParent)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(originalParent, transform.position);
    }

    private void InsertInGrid()
    {
        int closesIndex = 0;

        for (int i = 0; i < _originalParent.transform.childCount; i++)
        {
            if (Vector3.Distance(transform.position, _originalParent.GetChild(i).position) <
                Vector3.Distance(transform.position, _originalParent.GetChild(closesIndex).position))
            {
                closesIndex = i;
            }
        }

        transform.SetParent(_originalParent);
        transform.SetSiblingIndex(closesIndex);
    }    
}