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

    public Transform Target => _target;

    public GenericObjectPool<EnemyView> Pool { get; set; }

    public void SetTarget(Transform target)
    {
        OnSetTargetEvent?.Invoke(target);
    }

    public void Die()
    {
        Debug.Log("Enemy: Die");
        Pool.Release(this);
    }

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {        
        OnUpdateEvent?.Invoke();

        if (Vector3.Distance(Transform.position, Target.position) < _followDistance)
        {
            SetTarget(Target);
        }
        else
        {
            Stay();
        }
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

    private void Stay()
    {
        OnStayEvent?.Invoke();
    }    
}
