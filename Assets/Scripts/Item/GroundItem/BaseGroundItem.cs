using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class BaseGroundItem : BaseItem
{    
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    public override BaseItemObject Item
    {
        get => base.Item;
        set
        {
            base.Item = value;
            _spriteRenderer.sprite = value.UiDisplay;
        }
    }   

    private void Awake()
    {
        _spriteRenderer.sprite = Item?.UiDisplay;
    }
}
