using UnityEngine.InputSystem;
using MVP;
public abstract class BasePlayerPresenter : BasePresenter<IPlayerView>
{       
    public BasePlayerPresenter(IPlayerView view) : base(view)
    {
    }

    public abstract void StartingMove(InputAction.CallbackContext context);    

    public abstract void CancelingMove(InputAction.CallbackContext context);    

    public abstract void Jump(InputAction.CallbackContext context);
    public abstract void Attack(InputAction.CallbackContext context);
    public abstract void OpenInventory(InputAction.CallbackContext context);
    public abstract void Interact(InputAction.CallbackContext context);
    public abstract void CallDeath(int currentHealth);
}