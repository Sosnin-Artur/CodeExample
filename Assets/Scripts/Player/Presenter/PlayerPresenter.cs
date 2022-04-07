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
    private IInventoryModel _inventoryModel;
    private BaseEquipment _equipment;

    private PlayerInputAction _control;    

    public PlayerPresenter(
        IPlayerView view, 
        IPlayerModel model, 
        IHealthModel healthModel, 
        IInventoryModel inventoryModel,
        BaseEquipment equipment,
        PlayerInputAction control) : base(view)
    {        
        _playerModel = model;
        _inventoryModel = inventoryModel;
        _control = control;
        _equipment = equipment;
        
        _equipment.StartingUsingCallbackEvent += DisconnectControl;
        _equipment.EndingUsingCallbackEvent += ConnectControl;
        
        healthModel.CurrentHealth.Subscribe(x => CallDeath(x));
        inventoryModel.ItemToEquip.Subscribe(x => equipment.EquipItem(x, View.WeaponPlace));       
        
        Connect();
    }

    public void Connect()
    {        
        ConnectControl();
        ConnectView();
    }

    public void ConnectControl()
    {        
        _control.PlayerControl.Move.started += StartingMove;
        _control.PlayerControl.Move.canceled += CancelingMove;
        _control.PlayerControl.Jump.started += Jump;
        _control.PlayerControl.Attack.started += Attack;
        _control.PlayerControl.OpenInventory.started += OpenInventory;
        _control.PlayerControl.Interact.started += Interact;

        _control.Enable();
    }    

    public void ConnectView()
    {
        View.EnabledEvent += EnableControl;
        View.DisabledEvent += DisableControl;
        View.TakingItemEvent += GettingItem;        
    }    

    public void EnableControl()
    {
        _control.Enable();
    }

    public override void StartingMove(InputAction.CallbackContext context) 
    {        
        var direction = context.ReadValue<float>();
        _equipment.RotateAxisX(direction);
        View.Mover.MoveInDirectionX(direction);
    }

    public override void CancelingMove(InputAction.CallbackContext context)
    {        
        View.Mover.StopMove();        
    }

    public override void Jump(InputAction.CallbackContext context)
    {        
        View.Mover.OnJump();
    }

    public override void Attack(InputAction.CallbackContext obj)
    {
        if (View.Mover.IsGrounded() && !View.Mover.IsMoving)
        {
            _equipment.Use();
        }
    }

    public override void OpenInventory(InputAction.CallbackContext context)
    {        
        OpenInventory();        
    }    
    
    public void OpenInventory(bool absolute = false, bool value = true)
    {        
        if (absolute)
        {
            _inventoryModel.OpenInventory.Value = value;
        }
        else
        {
            _inventoryModel.OpenInventory.Value = !_inventoryModel.OpenInventory.Value;
        }        
    }

    public override void Interact(InputAction.CallbackContext context)
    {        
        if (View.IntersectedInteractableObject)
        {
            View.IntersectedInteractableObject.StartInteract(View.Transform);
            OpenInventory(true, View.IntersectedInteractableObject.IsActive);
        }        
    }

    public override void CallDeath(int currentHealth)
    {                
        if (currentHealth <= 0)
        {
            View.Die();
        }        
    }

    public override void Dispose()
    {
        DisableControl();
        DisconnectControl();
        DisconnectView();
    }

    private void GettingItem(BaseGroundItem item)
    {
        _inventoryModel.ItemToAdd.Value = item;
    }
    
    private void DisableControl()
    {
        _control.Disable();
    }

    private void DisconnectControl()
    {
        _control.PlayerControl.Move.started -= StartingMove;
        _control.PlayerControl.Move.canceled -= CancelingMove;
        _control.PlayerControl.Jump.started -= Jump;
        _control.PlayerControl.Attack.started -= Attack;
        _control.PlayerControl.OpenInventory.started -= OpenInventory;
        _control.PlayerControl.Interact.started -= Interact;
    }

    private void DisconnectView()
    {
        View.EnabledEvent -= EnableControl;
        View.DisabledEvent -= DisableControl;
        View.TakingItemEvent -= GettingItem;
    }    
}
