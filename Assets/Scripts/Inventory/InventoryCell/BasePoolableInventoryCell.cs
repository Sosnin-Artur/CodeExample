using ObjectPool;
using UnityEngine.EventSystems;

public class BasePoolableInventoryCell : BaseInventoryCell, IPoolable
{
    public virtual void Spawn()
    {
        gameObject.SetActive(true);
    }

    public virtual void Release()
    {
        gameObject.SetActive(false);
    }    
}