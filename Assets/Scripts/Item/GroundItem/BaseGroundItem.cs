using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class BaseGroundItem : MonoBehaviour
{
    [SerializeField]
    private BaseItemObject _item;
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    public virtual BaseItemObject Item
    {
        get => _item;
        set
        {
            _item = value;
            _spriteRenderer.sprite = value.UiDisplay;
        }
    }

    private void Awake()
    {
        _spriteRenderer.sprite = _item?.UiDisplay;
    }
}
