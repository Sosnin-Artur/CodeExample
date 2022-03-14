using System;
using UnityEngine;

public abstract class BaseUsableItem : BaseItem
{
    public virtual event Action OnStartingUsingCallbackEvent;
    public virtual event Action OnEndingUsingCallbackEvent;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    public virtual void SetUp(BaseItemObject item)
    {
        _spriteRenderer.sprite = item.UiDisplay;
    }

    public abstract void Rotate(Quaternion angle);

    public abstract void Use();
}