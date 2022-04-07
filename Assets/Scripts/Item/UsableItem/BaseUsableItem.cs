using System;
using UnityEngine;

public abstract class BaseUsableItem : BaseItem
{
    public virtual event Action StartingUsingCallbackEvent;
    public virtual event Action EndingUsingCallbackEvent;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    
    public virtual void SetUp(BaseItemObject item)
    {
        _spriteRenderer.sprite = item.UiDisplay;
    }

    public abstract void Rotate(Quaternion angle);    

    public abstract void Use();

    protected virtual void InvokeStartingUsingCallbackEvent()
    {
        StartingUsingCallbackEvent?.Invoke();
    }

    protected virtual void InvokeEndingingUsingCallbackEvent()
    {
        EndingUsingCallbackEvent?.Invoke();
    }
}