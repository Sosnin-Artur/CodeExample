using System;
using System.Reactive.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerPresenter : BasePlayerPresenter
{
    private IPlayerModel _playerModel;    
    private PlayerInputAction _control;    

    public PlayerPresenter(IPlayerView view
        , IPlayerModel model, IHealthModel healthModel, PlayerInputAction control) : base(view)
    {        
        _playerModel = model;
        _control = control;             

        healthModel.CurrentHealth.Subscribe(x => Die(x));
        
        Initialize();
    }    

    public override void OnMoveStarted(InputAction.CallbackContext context) 
    {        
        var direction = context.ReadValue<float>();
        View.MoveInDirectionX(direction);
    }

    public override void OnMoveCancelled(InputAction.CallbackContext context)
    {        
        View.StopMove();        
    }

    public override void OnJump(InputAction.CallbackContext context)
    {
        View.OnJump();
    }

    public override void Die(int currentHealth)
    {        
        if (currentHealth <= 0)
        {
            View.Die();
        }        
    }

    public override void Dispose()
    {        
        DisableControl();

        _control.PlayerControl.Move.started -= OnMoveStarted;
        _control.PlayerControl.Move.canceled -= OnMoveCancelled;
        _control.PlayerControl.Jump.started -= OnJump;

        View.OnEnabledEvent -= EnableControl;
        View.OnDisabledEvent -= DisableControl;

    }

    private void Initialize()
    {
        _control = new PlayerInputAction();

        _control.PlayerControl.Move.started += OnMoveStarted;
        _control.PlayerControl.Move.canceled += OnMoveCancelled;
        _control.PlayerControl.Jump.started += OnJump;

        View.OnEnabledEvent += EnableControl;
        View.OnDisabledEvent += DisableControl;        
    }

    private void EnableControl()
    {
        _control.Enable();
    }

    private void DisableControl()
    {
        _control.Disable();
    }    
}
