using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class EnemyView : MonoBehaviour, IEnemyView
{        
    public event Action<EnemyStates> AtackingEvent;   
    public event Action UpdatingEvent;

    [SerializeField]
    private float _followDistance = 10.0f;
    [SerializeField]
    private Mover _mover;
    [SerializeField]
    private Transform _target = null;
    [SerializeField]
    private LayerMask _attackMask;
    [SerializeField]
    private LayerChechker _layerChecker;
    [SerializeField]
    private BaseLootSystem _looter;

    private Transform _transform;        

    public Mover Mover => _mover;    
    public Transform Transform => _transform;
    public GameObject GameObject => gameObject;
    public float FollowDistance => _followDistance;

    public ReactiveProperty<Transform> Target { get; private set; }    
       
    public void Awake()
    {        
        _transform = transform;

        Target = new ReactiveProperty<Transform>(null);
    }

    public void Die()
    {
        _looter.GetItems();
    }

    private void Update()
    {        
        UpdatingEvent?.Invoke();                
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_layerChecker.IsInLayerMask(_attackMask, collision.gameObject))
        {
            Atack();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (_layerChecker.IsInLayerMask(_attackMask, collision.gameObject))
        {                        
            Target.Value = collision.gameObject.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {        
        if (_layerChecker.IsInLayerMask(_attackMask, collision.gameObject))
        {                        
            Target.Value = null;
        }
    }

    private void Atack()
    {
        AtackingEvent?.Invoke(EnemyStates.Attack);
    }    
}
