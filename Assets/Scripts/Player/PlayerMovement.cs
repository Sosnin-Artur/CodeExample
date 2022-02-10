using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{    
    [SerializeField]    
    private PlayerInputAction _control;
    [SerializeField]
    [Tooltip("Curve for change movement initial speed")]
    private AnimationCurve _speedCurve = AnimationCurve.Constant(0, 1, 10);
    [SerializeField]
    [Tooltip("Curve for change velocity during jumping")]
    private AnimationCurve _jumpCurve = AnimationCurve.Constant(0, 1, 10);
    [SerializeField]
    [Tooltip("Reference to component of trigger to check ground")]
    private GroundChecker _groundChecker;    
    
    private Rigidbody2D _rb;
    private SpriteRenderer _sprite;

    private float _directionX = 0;        

    private IEnumerator _moveCoroutine;    

    private void Awake()
    {
        _control = new PlayerInputAction();
        _control.PlayerControl.Move.started += OnMoveStarted;
        _control.PlayerControl.Move.canceled += OnMoveCancelled;        
        _control.PlayerControl.Jump.started += OnJump;
        
        _rb = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();                
        
        _moveCoroutine = Move();        
    }
    
    private void OnMoveStarted(InputAction.CallbackContext context)
    {        
        _directionX = context.ReadValue<float>();        
        MoveInDirectionX(_directionX);        
    }

    private void OnMoveCancelled(InputAction.CallbackContext context)
    {        
        _directionX = 0;              
        StopCoroutine(_moveCoroutine);
        _rb.velocity = new Vector2(0, _rb.velocity.y);
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (IsGrounded())
        {            
            StartCoroutine(Jump());
        }                
    }

    private bool IsGrounded()
    {                       
        return _groundChecker.IsGrounded;
    }
    
    private void OnEnable()
    {
        _control.Enable();
    }
    private void OnDisable()
    {
        _control.Disable();
    }

    private IEnumerator Move()
    {
        var moveTimeElapsed = 0.0f;
         
        while (true)
        {
            yield return new WaitForFixedUpdate();
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

        do 
        {            
            _rb.velocity = new Vector2(                    
                    _rb.velocity.x,
                    _jumpCurve.Evaluate(jumpTimeElapsed)
                    );
            jumpTimeElapsed += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        while (!IsGrounded());       
    }

    public void MoveInDirectionX(float direction)
    {
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
}

