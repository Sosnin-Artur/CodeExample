using System;
using UnityEngine;

public class InventoryActivator : BaseInteractableObject
{
    [SerializeField]
    private float _deactivateDistance;
    [SerializeField]
    private Transform _transform;
    [SerializeField]
    private InventoryView _inventory;

    private Transform _interactor;

    private bool _isActive = false;

    public override bool IsActive => _isActive;

    public void Update()
    {
        if (_interactor)
        {
            if (Vector2.Distance(_transform.position, _interactor.position)
                > _deactivateDistance)
            {
                EndInteract();
            }
        }        
    }

    public override void StartInteract(Transform interactor)
    {
        _isActive = !_isActive;
        _interactor = interactor;
        _inventory.SwitchInventoryActivity(_isActive);
    }

    public override void EndInteract()
    {
        _isActive = false;
        _interactor = null;
        _inventory.SwitchInventoryActivity(false);
    }    
}
