using UnityEngine;

public abstract class BaseInteractableObject : MonoBehaviour
{
    public abstract bool IsActive { get; }
    public abstract void StartInteract(Transform interactor);
    public abstract void EndInteract();
}
