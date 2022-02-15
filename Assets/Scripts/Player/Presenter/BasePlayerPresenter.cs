using UnityEngine.InputSystem;

public abstract class BasePlayerPresenter : BasePresenter<IPlayerView>
{    
    public BasePlayerPresenter(IPlayerView view) : base(view)
    {
    }

    public abstract void OnMoveStarted(InputAction.CallbackContext context);    

    public abstract void OnMoveCancelled(InputAction.CallbackContext context);    

    public abstract void OnJump(InputAction.CallbackContext context);    
}