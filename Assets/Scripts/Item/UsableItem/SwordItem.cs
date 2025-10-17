using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using Zenject;
using UnityEngine.InputSystem;

public class SwordItem : BaseUsableItem
{    
    [SerializeField]
    private float _duration;
    [SerializeField]
    private Rigidbody2D _rb;
    [SerializeField]
    private Collider2D _collider;    
    [SerializeField]
    private Transform _endPoint;

    private Transform _axis;
    private Sequence _sequence;        

    public void Awake()
    {
        _axis = transform.parent.parent;
        InitSequence();
    }        

    public void InitSequence()
    {
        var startPosition = transform.parent.position;

        _sequence = DOTween.Sequence();        
        _sequence.PrependCallback(() => InvokeStartingUsingCallbackEvent());
        _sequence.PrependCallback(() => _collider.enabled = true);
        _sequence.Append(DOTweenModulePhysics2D.DOMove(_rb, _endPoint.position, _duration));
        _sequence.Append(DOTweenModulePhysics2D.DOMove(_rb, transform.position, _duration));
        _sequence.AppendCallback(() => _collider.enabled = false);
        _sequence.AppendCallback(() => _sequence.Rewind());
        _sequence.AppendCallback(() => InvokeEndingingUsingCallbackEvent());
        _sequence.AppendCallback(() => transform.position = startPosition);
        _sequence.Pause();
    }    

    public override void Use()
    {        
        if (!_sequence.IsPlaying())
        {
            InitSequence();
            _sequence.Play();           
        }        
    }

    public override void Rotate(Quaternion angle)
    {        
        _axis.rotation = angle;
    }
}

