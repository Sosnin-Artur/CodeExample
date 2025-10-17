using System;
using UnityEngine;

public abstract class BaseProjectile : MonoBehaviour
{
    [SerializeField]
    private float _startForce = 1.0f;
    [SerializeField]
    protected Rigidbody2D Rigidbody;

    public virtual void Fire(Vector2 direction)
    {
        Rigidbody.AddForce(_startForce * direction, ForceMode2D.Impulse);
    }
}