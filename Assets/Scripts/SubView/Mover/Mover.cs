using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour, IMover
{
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

    private IEnumerator _moveCoroutine;
    private IEnumerator _jumpCoroutine;
    private float _directionX;

    public bool IsMoving { get; private set; }

    public void Awake()
    {
        _moveCoroutine = Move();
        _jumpCoroutine = Jump();
    }

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
        IsMoving = true;
        
        StartCoroutine(_moveCoroutine);
    }

    public void StopMove()
    {
        IsMoving = false;
        
        StopCoroutine(_moveCoroutine);        
        _rb.SetVelocityX(0);
    }

    public void OnJump()
    {
        if (IsGrounded())
        {
            _jumpCoroutine = Jump();
            StartCoroutine(_jumpCoroutine);            
        }
    }

    public bool IsGrounded()
    {
        return _groundChecker.IsGrounded;
    }    

    private IEnumerator Move()
    {
        var moveTimeElapsed = 0.0f;
        var wait = new WaitForFixedUpdate();

        while (true)
        {
            yield return wait;
            
            _rb.SetVelocityX(_directionX * _speedCurve.Evaluate(moveTimeElapsed));
            moveTimeElapsed += Time.fixedDeltaTime;
        }
    }

    private IEnumerator Jump()
    {
        var jumpTimeElapsed = 0.0f;
        var wait = new WaitForFixedUpdate();

        do
        {            
            _rb.SetVelocityY(_jumpCurve.Evaluate(jumpTimeElapsed));
            jumpTimeElapsed += Time.fixedDeltaTime;

            yield return wait;
        }
        while (!IsGrounded());

        StopCoroutine(_jumpCoroutine);

    }

    public void Dispose()
    {
        StopCoroutine(_moveCoroutine);
        StopCoroutine(_jumpCoroutine);
    }
}
