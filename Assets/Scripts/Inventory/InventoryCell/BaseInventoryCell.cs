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
    public event Action<BaseInventoryCell, BaseItemObject, Vector2> OnEjectingEvent;
    public event Action<BaseItemObject> OnEquipingEvent;

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
        _nameField.text = item.ItemData.Name;
        _icon.sprite = item.UiDisplay;        
    }    

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
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
            var comp = eventData.hovered[hoveredCount - 1].GetComponentInChildren<InventoryView>();

            if (In((RectTransform)_originalParent))
            {
                InsertInGrid();
            }
            else if (comp != null)
            {
                comp.AddItem(_item);
            }
            else
            {
                OnEjectingEvent?.Invoke(this, _item, eventData.position);
            }
        }                               
        else
        {                        
            OnEjectingEvent?.Invoke(this, _item, eventData.position);            
        }        
    }

    public void OnPointerClick(PointerEventData eventData)
    {               
        var comp = eventData.hovered[eventData.hovered.Count - 1];
        
        if (comp != null)
        {            
            OnEquipingEvent?.Invoke(_item);            
        }
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