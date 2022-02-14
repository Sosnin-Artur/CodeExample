using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerPresenter : IPlayerPresenter, IInitializable
{
    private IPlayerModel _playerModel;
    private IPlayerView _playerView;
    private PlayerInputAction _control;    

    public PlayerPresenter(IPlayerView view, IPlayerModel model, PlayerInputAction control)
    {
        _playerView = view;
        _playerModel = model;
        _control = control;             
    }

    [Inject]
    public void Initialize()
    {
        _control = new PlayerInputAction();
        
        _control.PlayerControl.Move.started += OnMoveStarted;
        _control.PlayerControl.Move.canceled += OnMoveCancelled;
        _control.PlayerControl.Jump.started += OnJump;        

        _playerView.OnEnableEvent += OnEnable;
        _playerView.OnDisableEvent += OnDisable;
    }

    public void OnMoveStarted(InputAction.CallbackContext context)
    {        
        var direction = context.ReadValue<float>();
        _playerView.MoveInDirectionX(direction);
    }

    public void OnMoveCancelled(InputAction.CallbackContext context)
    {        
        _playerView.StopMove();        
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        _playerView.OnJump();
    }

    private void OnEnable()
    {
        _control.Enable();
    }
    private void OnDisable()
    {
        _control.Disable();
    }    
}
