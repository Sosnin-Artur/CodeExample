using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyView : MonoBehaviour, IEnemyView
{    
    public event Action<Transform> OnSetTargetEvent;
    public event Action OnAtackEvent;
    public event Action OnStayEvent;
    public event Action OnUpdateEvent;

    [SerializeField]
    private float _followDistance = 10.0f;
    [SerializeField]
    private Mover _mover;
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private LayerMask _attackMask;

    private Transform _transform;        

    public Mover Mover => _mover;    
    public Transform Transform => _transform;
    public GameObject GameObject => gameObject;
    public float FollowDistance => _followDistance;    
    
    public Transform Target
    {
        get
        {
            return _target;
        }
        set
        {
            _target = value;
        }
    }    

    public void Die()
    {
        Debug.Log("Enemy: Die");                
    }

    private void Awake()
    {        
        _transform = transform;                
    }

    private void Update()
    {        
        OnUpdateEvent?.Invoke();                
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsInLayerMask(_attackMask, collision.gameObject))
        {
            Atack();
        }
    }

    private bool IsInLayerMask(LayerMask mask, GameObject obj)
    {
        return ((mask.value & (1 << obj.layer)) > 0);
    }    

    private void Atack()
    {
        OnAtackEvent?.Invoke();
    }    
}
