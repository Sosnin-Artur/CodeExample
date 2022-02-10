using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPresenter : IPlayerPresenter
{
    private PlayerModel _playerModel;
    private PlayerView _playerView;

    public PlayerPresenter(PlayerView view, PlayerModel model)
    {
        _playerView = view;
        _playerModel = model;

        InitializeModel();
    }

    private void InitializeModel()
    {
        _playerModel.Control = new PlayerInputAction();
        
        _playerModel.Control.PlayerControl.Move.started += OnMoveStarted;
        _playerModel.Control.PlayerControl.Move.canceled += OnMoveCancelled;
        _playerModel.Control.PlayerControl.Jump.started += OnJump;

        _playerModel.Control.Enable();
    }

    private void OnMoveStarted(InputAction.CallbackContext context)
    {        
        var direction = context.ReadValue<float>();
        _playerView.MoveInDirectionX(direction);
    }

    private void OnMoveCancelled(InputAction.CallbackContext context)
    {        
        _playerView.StopMove();        
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        _playerView.OnJump();
    }
    private void OnEnable()
    {
        _playerModel.Control.Enable();
    }
    private void OnDisable()
    {
        _playerModel.Control.Disable();
    }
}
