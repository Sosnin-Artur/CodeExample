using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour, IPlayerView
{            
    public event Action OnEnabledEvent;
    public event Action OnDisabledEvent;

    [SerializeField]
    [Tooltip("Curve for change movement initial speed")]
    private AnimationCurve _speedCurve = AnimationCurve.Constant(0, 1, 10);
    [SerializeField]
    [Tooltip("Curve for change velocity during jumping")]
    private AnimationCurve _jumpCurve = AnimationCurve.Constant(0, 1, 10);
    [SerializeField]
    [Tooltip("Reference to component of trigger to check ground")]
    private GroundChecker _groundChecker;
    [SerializeField]
    [Tooltip("Rigidbody for move")]
    private Rigidbody2D _rb;
    [SerializeField]
    [Tooltip("SpriteRenderer of current object")]
    private SpriteRenderer _sprite;
    [SerializeField]
    [Tooltip("View of health system of player")]
    private HealthView _healthView;


    private IEnumerator _moveCoroutine;
    private float _directionX;
    
    public void MoveInDirectionX(float direction)
    {        
        _directionX = direction;
        if (direction > 0)
        {
            _sprite.flipX = true;
        }
        else if (direction < 0)
        {
            _sprite.flipX = false;
        }
        StartCoroutine(_moveCoroutine);

    }

    public void StopMove()
    {
        StopCoroutine(_moveCoroutine);
        _rb.velocity = new Vector2(0, _rb.velocity.y);
    }

    public void OnJump()
    {                    
        if (IsGrounded())
        {
            StartCoroutine(Jump());
        }
    }

    public bool IsGrounded()
    {
        return _groundChecker.IsGrounded;
    }

    private void Awake()
    {                        
        _moveCoroutine = Move();      
        _healthView.Presenter.OnDeathEvent += OnDeath;
    }    

    private void OnEnable()
    {
        OnEnabledEvent?.Invoke();

    }

    private IEnumerator Move()
    {
        var moveTimeElapsed = 0.0f;
        var wait = new WaitForFixedUpdate();

        while (true)
        {
            yield return wait;
            _rb.velocity = new Vector2(
                    _directionX * _speedCurve.Evaluate(moveTimeElapsed),
                    _rb.velocity.y
                    );
            moveTimeElapsed += Time.fixedDeltaTime;            
        }
    }

    private IEnumerator Jump()
    {
        var jumpTimeElapsed = 0.0f;                
        var wait = new WaitForFixedUpdate();

        do 
        {            
            _rb.velocity = new Vector2(                    
                    _rb.velocity.x,
                    _jumpCurve.Evaluate(jumpTimeElapsed)
                    );
            jumpTimeElapsed += Time.fixedDeltaTime;
            yield return wait;
        }
        while (!IsGrounded());       
    }

    private void OnDeath()
    {
        Debug.Log("Player: Death");

    }

    private void OnDisable()
    {
        OnDisabledEvent?.Invoke();
    }
}

