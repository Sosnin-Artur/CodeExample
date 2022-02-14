using UnityEngine.InputSystem;

public interface IPlayerPresenter
{
    void OnMoveStarted(InputAction.CallbackContext context);    

    void OnMoveCancelled(InputAction.CallbackContext context);    

    void OnJump(InputAction.CallbackContext context);    
}