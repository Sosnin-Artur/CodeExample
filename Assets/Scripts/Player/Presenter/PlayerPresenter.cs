using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerPresenter : BasePlayerPresenter, IInitializable
{
    private IModel _playerModel;    
    private PlayerInputAction _control;    

    public PlayerPresenter(IPlayerView view, IPlayerModel model, PlayerInputAction control) : base(view)
    {        
        _playerModel = model;
        _control = control;             

        Initialize();
    }
    
    public void Initialize()
    {
        _control = new PlayerInputAction();
        
        _control.PlayerControl.Move.started += OnMoveStarted;
        _control.PlayerControl.Move.canceled += OnMoveCancelled;
        _control.PlayerControl.Jump.started += OnJump;        

        View.OnEnabledEvent += EnableControl;
        View.OnDisabledEvent += DisableControl;
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

    private void EnableControl()
    {
        _control.Enable();
    }

    private void DisableControl()
    {
        _control.Disable();
    }    
}
